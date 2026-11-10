using Counter.Domain;
using VSA.Infrastructure;

namespace Counter.Infrastructure;

public sealed class TallyRepository(TallyStore store) : IRepository<Tally, TallyId> {
    readonly List<(Tally Tally, bool Add)> pending = [];

    public Task<Tally?> GetAsync(TallyId id, CancellationToken ct) =>
        Task.FromResult(store.Tallies.GetValueOrDefault(id));

    public Task<List<Tally>> ListAsync(Func<Tally, bool> where, CancellationToken ct) =>
        Task.FromResult(store.Tallies.Values.Where(where).ToList());

    public void Add(Tally aggregate) => pending.Add((aggregate, true));

    public void Remove(Tally aggregate) => pending.Add((aggregate, false));

    public Task SaveChangesAsync(CancellationToken ct) {
        foreach (var (tally, add) in pending) {
            if (add) {
                store.Tallies[tally.Id] = tally;
            } else {
                store.Tallies.TryRemove(tally.Id, out _);
            }
        }
        pending.Clear();
        return Task.CompletedTask;
    }
}
