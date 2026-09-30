using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Rubric criteria entities.
/// </summary>
internal sealed partial record RubricCriteriaDto
{
    [JsonPropertyName("criterion_use_range")]
    public bool? CriterionUseRange { get; init; }
    public string? Description { get; init; }
    public string? Id { get; init; }

    [JsonPropertyName("learning_outcome_id")]
    public string? LearningOutcomeId { get; init; }

    [JsonPropertyName("long_description")]
    public string? LongDescription { get; init; }
    public int? Points { get; init; }
    public IList<RubricRatingDto>? Ratings { get; init; }

    [JsonPropertyName("vendor_guid")]
    public string? VendorGuid { get; init; }
}
