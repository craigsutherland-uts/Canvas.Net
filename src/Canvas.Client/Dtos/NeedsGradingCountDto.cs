using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Needs grading count entities.
/// </summary>
internal sealed partial record NeedsGradingCountDto
{
    [JsonPropertyName("needs_grading_count")]
    public int? Count { get; init; }

    [JsonPropertyName("section_id")]
    public string? SectionId { get; init; }
}