using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Grade entities.
/// </summary>
internal sealed partial record GradeDto
{
    [JsonPropertyName("current_grade")]
    public string? CurrentGrade { get; init; }

    [JsonPropertyName("current_score")]
    public double? CurrentScore { get; init; }

    [JsonPropertyName("final_grade")]
    public string? FinalGrade { get; init; }

    [JsonPropertyName("final_score")]
    public double? FinalScore { get; init; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; init; }

    [JsonPropertyName("unposted_current_grade")]
    public string? UnpostedCurrentGrade { get; init; }

    [JsonPropertyName("unposted_current_score")]
    public double? UnpostedCurrentScore { get; init; }

    [JsonPropertyName("unposted_final_grade")]
    public string? UnpostedFinalGrade { get; init; }

    [JsonPropertyName("unposted_final_score")]
    public double? UnpostedFinalScore { get; init; }
}