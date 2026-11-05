using VSA.Domain;
using VSA.Handlers.Requests;

namespace Counter.Slices.GetStatus;

sealed class GetStatusHandler : RoutedHandler<GetStatusRequest, GetStatusResponse> {
    protected override Task<Result<GetStatusResponse>> HandleRequestAsync(GetStatusRequest request, CancellationToken ct) =>
        Task.FromResult<Result<GetStatusResponse>>(new GetStatusResponse("Counter", "Ready"));
}
