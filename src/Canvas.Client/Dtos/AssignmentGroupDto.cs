using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Assignment group entities.
/// </summary>
internal sealed partial record AssignmentGroupDto
{
    public IList<int>? Assignments { get; init; }

    [JsonPropertyName("group_weight")]
    public int? GroupWeight { get; init; }
    public  string ? Id { get; init; }

    [JsonPropertyName("integration_data")]
    public object? IntegrationData { get; init; }
    public string? Name { get; init; }
    public int? Position { get; init; }
    public GradingRulesDto? Rules { get; init; }

    [JsonPropertyName("sis_source_id")]
    public string? SisSourceId { get; init; }
}