using VSA.Contracts.Requests;

namespace Counter.Contracts.Requests.GetStatus;

public sealed record GetStatusRequest : IRequest<GetStatusResponse> {
    public static HttpMethod Method => HttpMethod.Get;
    public static string Route => "/api/counter/status";
}
