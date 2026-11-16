namespace Consumer.Features.Counter.Infrastructure;

using System.Collections.Concurrent;
using Consumer.Features.Counter.Domain;

public sealed class TallyStore(params Tally[] tallies) {
    internal ConcurrentDictionary<TallyId, Tally> Tallies { get; } = new(tallies.ToDictionary(t => t.Id));
}
