namespace Consumer.Tests;

using Consumer.App.Host;
using Consumer.Features.Counter.Presenters;
using Consumer.Features.Counter.Slices.Increment;
using Consumer.Features.Counter.Slices.Reset;
using Consumer.Plugins.Journal.Handlers.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Umpire.Vsa;
using Umpire.Vsa.Composition;
using Umpire.Vsa.Contracts.Commands;
using Umpire.Vsa.Presenters;

public sealed class CompositionTests {
    static ServiceProvider App(int max) {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Modules:Counter:Max"] = $"{max}" })
            .Build();
        var services = new ServiceCollection().AddSingleton<IConfiguration>(configuration);
        return new Vsa<VsaHost>().CreateHost().Compose(services, new ConsumerApp()).BuildServiceProvider();
    }

    [Test]
    public async Task The_journal_plugin_writes_each_increment() {
        await using var app = App(max: 3);
        await using var scope = app.CreateAsyncScope();
        var bus = scope.ServiceProvider.GetRequiredService<IBus>();

        await bus.Send(new IncrementRequest(2));
        await bus.Send(new IncrementRequest(2));
        await bus.Send(new IncrementRequest(0));

        Assert.That(app.GetRequiredService<JournalLog>().Lines, Is.EqualTo(new[] {
            "IncrementRequest: Accepted",
            "IncrementRequest: Accepted counter.at_max",
            "IncrementRequest: Rejected counter.amount_not_positive",
        }));
    }

    [Test]
    public async Task The_presenter_gets_the_event_of_its_module() {
        await using var app = App(max: 3);
        await using var scope = app.CreateAsyncScope();
        var counter = scope.ServiceProvider.GetRequiredService<CounterPresenter>();

        await counter.LoadAsync();
        await counter.IncrementAsync(2);

        Assert.That((counter.Value, counter.Max), Is.EqualTo((2, 3)));
    }

    [Test]
    public async Task Another_module_sends_the_Reset_command() {
        await using var app = App(max: 3);
        await using var scope = app.CreateAsyncScope();
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
