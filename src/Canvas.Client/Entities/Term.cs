namespace Canvas.Client.Entities;
/// <summary>
/// The Term entity definition.
/// </summary>
public sealed partial record Term
{
    private TermDto? _source;
    private readonly ICanvasClient _client;
    internal Term(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the Term is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// 
    /// </summary>
    public DateTime? EndAt { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? Id { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Default Term&quot;
    /// </example>
    public string? Name { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 2012-06-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? StartAt { get; init; }

    internal static Term? From(ICanvasClient client, TermDto? dto)
    {
        if (dto == null)
            return null;
        return new Term(client)
        {
            EndAt = dto.EndAt,
            Id = dto.Id,
            Name = dto.Name,
            StartAt = dto.StartAt,
            _source = dto,
        };
    }

    internal static IList<Term> From(ICanvasClient client, IEnumerable<TermDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}