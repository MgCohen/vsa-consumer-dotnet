using VSA.Contracts;
using VSA.Handlers;

namespace Counter.Slices.Increment;

sealed class IncrementValidator : Validator<IncrementRequest> {
    static readonly Error NotPositive = new("counter.amount_not_positive", "The amount must be 1 or more.");

    public override Error? Validate(IEnvelope<IncrementRequest> envelope) =>
        envelope.Message.Amount < 1 ? NotPositive : null;
}
