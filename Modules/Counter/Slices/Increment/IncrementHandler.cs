using Counter.Domain;
using VSA.Contracts;
using VSA.Contracts.Events;
using VSA.Handlers.Requests;
using VSA.Infrastructure;

namespace Counter.Slices.Increment;

sealed class IncrementHandler(IRepository<Tally, TallyId> tallies, IIntegrationEventPublisher events)
    : RequestHandler<IncrementRequest, IncrementResponse> {
    static readonly Error NotFound = new("counter.not_found", "The counter does not exist.");
    static readonly Error AtMax = new("counter.at_max", "The counter is at its maximum.");

    public override async Task<Result<IncrementResponse>> HandleAsync(Envelope<IncrementRequest> envelope) {
        if (await tallies.GetAsync(TallyId.Main, envelope.Cancellation) is not { } tally) {
            return NotFound;
        }
        if (!tally.CanAdd(envelope.Message.Amount)) {
            return AtMax;
        }
        tally.Add(envelope.Message.Amount);
        tallies.Add(tally);
        await tallies.SaveChangesAsync(envelope.Cancellation);
        await events.PublishAsync(new CounterIncremented(tally.Value, tally.Max), envelope.Cancellation);
        return new IncrementResponse(tally.Value);
    }
}
