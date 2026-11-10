using Counter.Presenters;
using Counter.Slices.Increment;
using Counter.Slices.Reset;
using Journal.Handlers.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VSA.Composition;
using VSA.Contracts.Commands;
using VSA.Presenters;

namespace App.Tests;

public sealed class CompositionTests {
    static IHost App(int max) => VsaBuilder.Create($"--Modules:Counter:Max={max}").UseGenericHost().Build();

    [Test]
    public async Task The_journal_plugin_writes_each_increment() {
        using var app = App(max: 3);
        await using var scope = app.Services.CreateAsyncScope();
        var bus = scope.ServiceProvider.GetRequiredService<IBus>();

        await bus.Send(new IncrementRequest(2));
        await bus.Send(new IncrementRequest(2));
        await bus.Send(new IncrementRequest(0));

        Assert.That(app.Services.GetRequiredService<JournalLog>().Lines, Is.EqualTo(new[] {
            "IncrementRequest: Accepted",
            "IncrementRequest: Accepted counter.at_max",
            "IncrementRequest: Rejected counter.amount_not_positive",
        }));
    }

    [Test]
    public async Task The_presenter_gets_the_event_of_its_module() {
        using var app = App(max: 3);
        await using var scope = app.Services.CreateAsyncScope();
        var counter = scope.ServiceProvider.GetRequiredService<CounterPresenter>();

        await counter.LoadAsync();
        await counter.IncrementAsync(2);

        Assert.That((counter.Value, counter.Max), Is.EqualTo((2, 3)));
    }

    [Test]
    public async Task Another_module_sends_the_Reset_command() {
        using var app = App(max: 3);
        await using var scope = app.Services.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<IBus>().Send(new IncrementRequest(2));

        var reset = await scope.ServiceProvider.GetRequiredService<ICommandSender>().SendAsync(new ResetCommand(), CancellationToken.None);

        Assert.That(reset.Value, Is.EqualTo(new ResetResult(2)));
    }

    [Test]
    public void A_setting_that_is_not_valid_stops_the_start() {
        var start = Assert.Throws<InvalidOperationException>(() => App(max: 0));

        Assert.That(start!.Message, Does.Contain("Modules:Counter is not valid"));
    }
}
