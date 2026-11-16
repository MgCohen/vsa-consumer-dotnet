namespace Consumer.Features.Counter.Slices.Reset;

using Umpire.Vsa.Contracts.Commands;

public sealed record ResetCommand : ICommand<ResetResult>;
