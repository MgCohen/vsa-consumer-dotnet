using VSA.Domain;
using VSA.Handlers.Requests;

namespace Counter.Slices.Increment;

sealed class IncrementHandler : RoutedHandler<IncrementRequest, IncrementResponse> {
    protected override Task<Result<IncrementResponse>> HandleRequestAsync(IncrementRequest request, CancellationToken ct) =>
        Task.FromResult<Result<IncrementResponse>>(new IncrementResponse());
}
