namespace Consumer.Plugins.Journal;

using Consumer.Plugins.Journal.Contracts.Requests;
using Consumer.Plugins.Journal.Handlers.Requests;
using Microsoft.Extensions.DependencyInjection;
using Umpire.Messages.Handlers;
using Umpire.Vsa.Composition;
using Umpire.Vsa.Handlers.Requests;

public sealed class JournalPlugin : Plugin<JournalSettings> {
    public override void Configure(IServiceCollection services, JournalSettings settings) {
        services.AddSingleton(new JournalLog(settings.MaxLines));
        services.AddPipeline(new PipelineBuilder<Journaled>()
            .HandlersOf(typeof(RequestHandler<,>))
            .AfterStep<WriteJournal>());
    }
}
