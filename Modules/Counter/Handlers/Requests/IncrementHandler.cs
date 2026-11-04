using Counter.Contracts.Requests.Increment;
using VSA.Domain;
using VSA.Handlers.Requests;

namespace Counter.Handlers.Requests;

sealed class IncrementHandler : RoutedHandler<IncrementRequest, IncrementResponse> {
    protected override Task<Result<IncrementResponse>> HandleRequestAsync(IncrementRequest request, CancellationToken ct) =>
        Task.FromResult<Result<IncrementResponse>>(new IncrementResponse());
}
