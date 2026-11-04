using Counter.Contracts.Requests.GetStatus;
using VSA.Presenters;

namespace Counter.Presenters;

public sealed class CounterPresenter(IBus bus) : Presenter(bus) {
    public string Status { get; private set; } = "";
    public OpState Load { get; } = new();

    public Task LoadAsync() => Run(Load, async () => {
        var response = await Bus.Send(new GetStatusRequest());
        Status = response.Status;
    });
}
