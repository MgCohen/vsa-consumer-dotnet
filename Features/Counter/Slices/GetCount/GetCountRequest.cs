namespace Consumer.Features.Counter.Slices.GetCount;

using Umpire.Vsa.Contracts.Requests;

public sealed record GetCountRequest : IRequest<GetCountResponse> {
    public static HttpMethod Method => HttpMethod.Get;
    public static string Route => "/api/counter";
}
