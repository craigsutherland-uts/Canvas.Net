using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Grading rules entities.
/// </summary>
internal sealed partial record GradingRulesDto
{
    [JsonPropertyName("drop_highest")]
    public int? DropHighest { get; init; }

    [JsonPropertyName("drop_lowest")]
    public int? DropLowest { get; init; }

    [JsonPropertyName("never_drop")]
    public IList<int>? NeverDrop { get; init; }
}