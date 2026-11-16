namespace Consumer.Plugins.Journal;

using System.ComponentModel.DataAnnotations;

public sealed class JournalSettings {
    [Range(1, 10_000)]
    public int MaxLines { get; set; } = 100;
}
