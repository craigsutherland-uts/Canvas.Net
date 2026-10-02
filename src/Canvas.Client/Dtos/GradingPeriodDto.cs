using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Grading period entities.
/// </summary>
internal sealed partial record GradingPeriodDto
{
    [JsonPropertyName("close_date")]
    public DateTime? CloseDate { get; init; }

    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; init; }
    public int? Id { get; init; }

    [JsonPropertyName("is_closed")]
    public bool? IsClosed { get; init; }

    [JsonPropertyName("start_date")]
    public DateTime? StartDate { get; init; }
    public string? Title { get; init; }
    public double? Weight { get; init; }
}