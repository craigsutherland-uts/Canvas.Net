using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;

/// <summary>
/// A DTO for transferring Assignment override entities.
/// </summary>
internal sealed partial record AssignmentOverrideDto
{
    [JsonPropertyName("all_day")]
    public int? AllDay { get; init; }

    [JsonPropertyName("all_day_date")]
    public DateTime? AllDayDate { get; init; }

    [JsonPropertyName("assignment_id")]
    public int? AssignmentId { get; init; }

    [JsonPropertyName("course_section_id")]
    public int? CourseSectionId { get; init; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; init; }

    [JsonPropertyName("group_id")]
    public int? GroupId { get; init; }
    public int? Id { get; init; }

    [JsonPropertyName("lock_at")]
    public DateTime? LockAt { get; init; }

    [JsonPropertyName("student_ids")]
    public IList<int>? StudentIds { get; init; }
    public string? Title { get; init; }

    [JsonPropertyName("unlock_at")]
    public DateTime? UnlockAt { get; init; }
}