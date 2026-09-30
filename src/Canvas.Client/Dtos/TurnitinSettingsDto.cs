using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;

/// <summary>
/// A DTO for transferring Turnitin settings entities.
/// </summary>
internal sealed partial record TurnitinSettingsDto
{
    [JsonPropertyName("exclude_biblio")]
    public bool? ExcludeBiblio { get; init; }

    [JsonPropertyName("exclude_quoted")]
    public bool? ExcludeQuoted { get; init; }

    [JsonPropertyName("exclude_small_matches_type")]
    public string? ExcludeSmallMatchesType { get; init; }

    [JsonPropertyName("exclude_small_matches_value")]
    public int? ExcludeSmallMatchesValue { get; init; }

    [JsonPropertyName("internet_check")]
    public bool? InternetCheck { get; init; }

    [JsonPropertyName("journal_check")]
    public bool? JournalCheck { get; init; }

    [JsonPropertyName("originality_report_visibility")]
    public string? OriginalityReportVisibility { get; init; }

    [JsonPropertyName("s_paper_check")]
    public bool? SPaperCheck { get; init; }
}
