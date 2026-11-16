namespace Consumer.Features.Counter.Slices.Increment;

using Consumer.Plugins.Journal.Contracts.Requests;
using Umpire.Vsa.Contracts.Requests;

public sealed record IncrementRequest(int Amount) : Journaled, IRequest<IncrementResponse> {
    public static HttpMethod Method => HttpMethod.Post;
    public static string Route => "/api/counter/increment";
}
