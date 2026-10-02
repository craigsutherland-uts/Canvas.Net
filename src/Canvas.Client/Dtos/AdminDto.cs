using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Admin entities.
/// </summary>
internal sealed partial record AdminDto
{
    public int? Id { get; init; }
    public string? Role { get; init; }
    public UserDto? User { get; init; }

    [JsonPropertyName("workflow_state")]
    public string? WorkflowState { get; init; }
}