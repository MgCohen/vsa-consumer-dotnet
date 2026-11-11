using Journal.Contracts.Requests;
using VSA.Contracts;
using VSA.Handlers;

namespace Journal.Handlers.Requests;

public sealed class WriteJournal(JournalLog log) : AfterStep<Journaled> {
    public override Task AfterAsync(IEnvelope<Journaled> envelope) {
        var error = envelope.Error ?? envelope.Result?.Error;
        var outcome = error is null ? envelope.State.ToString() : $"{envelope.State} {error.Code}";
        log.Write($"{envelope.Message.GetType().Name}: {outcome}");
        return Task.CompletedTask;
    }
}
