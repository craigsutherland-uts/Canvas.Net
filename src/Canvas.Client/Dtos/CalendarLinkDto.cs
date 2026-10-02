using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Calendar link entities.
/// </summary>
internal sealed partial record CalendarLinkDto
{
    public string? Ics { get; init; }
}