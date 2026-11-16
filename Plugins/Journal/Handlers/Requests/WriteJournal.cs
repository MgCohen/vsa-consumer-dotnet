namespace Consumer.Plugins.Journal.Handlers.Requests;

using Consumer.Plugins.Journal.Contracts.Requests;
using Umpire.Messages.Contracts;
using Umpire.Messages.Handlers;

public sealed class WriteJournal(JournalLog log) : AfterStep<Journaled> {
    public override Task AfterAsync(IEnvelope<Journaled> envelope) {
        var error = envelope.Error ?? envelope.Result?.Error;
        var outcome = error is null ? envelope.State.ToString() : $"{envelope.State} {error.Code}";
        log.Write($"{envelope.Message.GetType().Name}: {outcome}");
        return Task.CompletedTask;
    }
}
