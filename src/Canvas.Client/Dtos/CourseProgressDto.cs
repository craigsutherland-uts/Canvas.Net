using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Course progress entities.
/// </summary>
internal sealed partial record CourseProgressDto
{
    [JsonPropertyName("completed_at")]
    public DateTime? CompletedAt { get; init; }

    [JsonPropertyName("next_requirement_url")]
    public string? NextRequirementUrl { get; init; }

    [JsonPropertyName("requirement_completed_count")]
    public int? RequirementCompletedCount { get; init; }

    [JsonPropertyName("requirement_count")]
    public int? RequirementCount { get; init; }
}