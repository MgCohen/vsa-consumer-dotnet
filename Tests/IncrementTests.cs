using Counter.Domain;
using Counter.Slices.Increment;
using VSA.Testing.Unit;

namespace App.Tests;

public sealed class IncrementTests : VsaUnitTest {
    [Test]
    public async Task Adds_the_amount_and_publishes_CounterIncremented() {
        Repository<Tally, TallyId>().With(Tally.Create(TallyId.Main, max: 5));

        var result = await Handle(new IncrementRequest(2));

        Assert.That(result.Value, Is.EqualTo(new IncrementResponse(2)));
        var incremented = Events.OfType<CounterIncremented>().Single();
        Assert.That((incremented.Value, incremented.Max), Is.EqualTo((2, 5)));
    }

    [Test]
    public async Task Stops_at_the_maximum() {
        Repository<Tally, TallyId>().With(Tally.Create(TallyId.Main, max: 1));

        var result = await Handle(new IncrementRequest(2));

        Assert.That(result.Error?.Code, Is.EqualTo("counter.at_max"));
        Assert.That(Events.OfType<CounterIncremented>(), Is.Empty);
    }
}
