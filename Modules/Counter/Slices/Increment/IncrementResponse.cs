using VSA.Contracts.Requests;

namespace Counter.Slices.Increment;

// Add the output of the request as record parameters.
public sealed record IncrementResponse : IResponse;
