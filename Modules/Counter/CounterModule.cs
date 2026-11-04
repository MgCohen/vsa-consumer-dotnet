using VSA.Module;

namespace Counter;

// The root of the Counter module. The host finds it and calls Configure at start-up.
// When Domain has an aggregate, register the database here, for example:
//   module.AddDatabase(o => o.UseSqlite(settings.ConnectionString));
public sealed class CounterModule : Module {
    public override void Configure(ModuleBuilder module) {
    }
}
