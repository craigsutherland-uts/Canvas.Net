using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Term entities.
/// </summary>
internal sealed partial record TermDto
{
    [JsonPropertyName("end_at")]
    public DateTime? EndAt { get; init; }
    public int? Id { get; init; }
    public string? Name { get; init; }

    [JsonPropertyName("start_at")]
    public DateTime? StartAt { get; init; }
}