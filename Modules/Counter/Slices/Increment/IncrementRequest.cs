using VSA.Contracts.Requests;

namespace Counter.Slices.Increment;

// Add the input of the request as record parameters.
public sealed record IncrementRequest : IRequest<IncrementResponse> {
    public static HttpMethod Method => HttpMethod.Post;
    public static string Route => "/api/counter/increment";
}
