using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;

/// <summary>
/// A DTO for transferring External tool tag attributes entities.
/// </summary>
internal sealed partial record ExternalToolTagAttributesDto
{
    [JsonPropertyName("new_tab")]
    public bool? NewTab { get; init; }

    [JsonPropertyName("resource_link_id")]
    public string? ResourceLinkId { get; init; }
    public string? Url { get; init; }
}