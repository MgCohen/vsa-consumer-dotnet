namespace Consumer.Features.Counter.Slices.Increment;

using Umpire.Vsa.Contracts.Events;

public sealed record CounterIncremented(int Value, int Max) : IntegrationEvent;
