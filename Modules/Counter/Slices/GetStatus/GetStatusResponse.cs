using VSA.Contracts.Requests;

namespace Counter.Slices.GetStatus;

public sealed record GetStatusResponse(string Module, string Status) : IResponse;
