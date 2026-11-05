namespace Counter.Domain.Shared;

// The published domain of Counter: other modules read these types. A type here is immutable and holds data only.
public sealed record CounterKey(string Name);
