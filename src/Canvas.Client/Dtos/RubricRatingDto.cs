using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Rubric rating entities.
/// </summary>
internal sealed partial record RubricRatingDto
{
    public string? Description { get; init; }
    public string? Id { get; init; }

    [JsonPropertyName("long_description")]
    public string? LongDescription { get; init; }
    public int? Points { get; init; }
}