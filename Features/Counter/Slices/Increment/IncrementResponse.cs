namespace Consumer.Features.Counter.Slices.Increment;

using Umpire.Vsa.Contracts.Requests;

public sealed record IncrementResponse(int Value) : IResponse;
