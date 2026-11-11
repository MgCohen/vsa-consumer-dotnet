using Counter.Domain;
using VSA.Contracts;
using VSA.Handlers.Commands;
using VSA.Infrastructure;

namespace Counter.Slices.Reset;

sealed class ResetHandler(IRepository<Tally, TallyId> tallies) : CommandHandler<ResetCommand, ResetResult> {
    static readonly Error NotFound = new("counter.not_found", "The counter does not exist.");

    public override async Task<Result<ResetResult>> HandleAsync(Envelope<ResetCommand> envelope) {
        if (await tallies.GetAsync(TallyId.Main, envelope.Cancellation) is not { } tally) {
            return NotFound;
        }
        var previous = tally.Reset();
        tallies.Add(tally);
        await tallies.SaveChangesAsync(envelope.Cancellation);
        return new ResetResult(previous);
    }
}
