using Canvas.Client.Dtos;

namespace Canvas.Client.Entities;
/// <summary>
/// The External tool tag attributes entity definition.
/// </summary>
public sealed partial record ExternalToolTagAttributes
{
    private ExternalToolTagAttributesDto? _source;
    /// <summary>
    /// A flag indicating whether the ExternalToolTagAttributes is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// Whether or not there is a new tab for the external tool
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? NewTab { get; init; }
    /// <summary>
    /// the identifier for this tool_tag
    /// </summary> 
    /// <example>
    /// "ab81173af98b8c33e66a"
    /// </example>
    public string? ResourceLinkId { get; init; }
    /// <summary>
    /// URL to the external tool
    /// </summary> 
    /// <example>
    /// "http://instructure.com"
    /// </example>
    public string? Url { get; init; }

    internal static ExternalToolTagAttributes? From(ExternalToolTagAttributesDto? dto)
    {
        if (dto == null)
            return null;
        return new ExternalToolTagAttributes
        {
            NewTab = dto.NewTab,
            ResourceLinkId = dto.ResourceLinkId,
            Url = dto.Url,
            _source = dto,
        };
    }
}