using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Assignment date entities.
/// </summary>
internal sealed partial record AssignmentDateDto
{
    public bool? Base { get; init; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; init; }
    public int? Id { get; init; }

    [JsonPropertyName("lock_at")]
    public DateTime? LockAt { get; init; }
    public string? Title { get; init; }

    [JsonPropertyName("unlock_at")]
    public DateTime? UnlockAt { get; init; }
}