using VSA.Contracts.Requests;

namespace Counter.Contracts.Requests.Increment;

// Add the input of the request as record parameters.
public sealed record IncrementRequest : IRequest<IncrementResponse> {
    public static HttpMethod Method => HttpMethod.Post;
    public static string Route => "/api/counter/increment";
}
