namespace Consumer.Features.Counter;

using System.ComponentModel.DataAnnotations;

public sealed class CounterSettings {
    [Range(1, 1_000)]
    public int Max { get; set; } = 10;
}
