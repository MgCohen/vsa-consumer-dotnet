namespace Consumer.Features.Counter.Slices.GetCount;

using Umpire.Vsa.Contracts.Requests;

public sealed record GetCountResponse(int Value, int Max) : IResponse;
