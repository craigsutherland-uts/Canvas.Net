using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Rubric settings entities.
/// </summary>
internal sealed partial record RubricSettingsDto
{
    public int? Id { get; init; }
    public string? Title { get; init; }

    [JsonPropertyName("points_possible")]
    public double? PointsPossible { get; init; }

    [JsonPropertyName("free_form_criterion_comments")]
    public bool? FreeFormCriterionComments { get; init; }

    [JsonPropertyName("hide_score_total")]
    public bool? HideScoreTotal { get; init; }

    [JsonPropertyName("hide_points")]
    public bool? HidePoints { get; init; }
}