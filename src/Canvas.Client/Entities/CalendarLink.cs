namespace Canvas.Client.Entities;
/// <summary>
/// The Calendar link entity definition.
/// </summary>
public sealed partial record CalendarLink
{
    private CalendarLinkDto? _source;
    private readonly ICanvasClient _client;
    internal CalendarLink(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the CalendarLink is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// The URL of the calendar in ICS format
    /// </summary> 
    /// <example>
    /// &quot;https://canvas.instructure.com/feeds/calendars/course_abcdef.ics&quot;
    /// </example>
    public string? Ics { get; init; }

    internal static CalendarLink? From(ICanvasClient client, CalendarLinkDto? dto)
    {
        if (dto == null)
            return null;
        return new CalendarLink(client)
        {
            Ics = dto.Ics,
            _source = dto,
        };
    }

    internal static IList<CalendarLink> From(ICanvasClient client, IEnumerable<CalendarLinkDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}