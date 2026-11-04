using Counter.Contracts.Requests.GetStatus;
using VSA.Testing.Unit;

namespace Consumer.Tests;

// A unit test of one slice: the request goes to its handler, with the default fakes.
public sealed class GetStatusTests : VsaUnitTest {
    [Test]
    public async Task The_counter_module_is_ready() {
        var result = await Handle(new GetStatusRequest());

        Assert.That(result.Value, Is.EqualTo(new GetStatusResponse("Counter", "Ready")));
    }
}
