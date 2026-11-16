namespace Consumer.Features.Counter;

using Consumer.Features.Counter.Domain;
using Consumer.Features.Counter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Umpire.Kernel;
using Umpire.Vsa.Infrastructure;

public sealed class CounterModule : Module<CounterSettings> {
    public override void Configure(IServiceCollection services, CounterSettings settings) {
        services.AddSingleton(new TallyStore(Tally.Create(TallyId.Main, settings.Max)));
        services.AddScoped<IRepository<Tally, TallyId>, TallyRepository>();
    }
}
