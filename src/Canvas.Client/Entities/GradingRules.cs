namespace Canvas.Client.Entities;
/// <summary>
/// The Grading rules entity definition.
/// </summary>
public sealed partial record GradingRules
{
    private GradingRulesDto? _source;
    private readonly ICanvasClient _client;
    internal GradingRules(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the GradingRules is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// Number of highest scores to be dropped for each user.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? DropHighest { get; init; }
    /// <summary>
    /// Number of lowest scores to be dropped for each user.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? DropLowest { get; init; }
    /// <summary>
    /// Assignment IDs that should never be dropped.
    /// </summary> 
    /// <example>
    /// [33, 17, 24]
    /// </example>
    public IList<int>? NeverDrop { get; init; }

    internal static GradingRules? From(ICanvasClient client, GradingRulesDto? dto)
    {
        if (dto == null)
            return null;
        return new GradingRules(client)
        {
            DropHighest = dto.DropHighest,
            DropLowest = dto.DropLowest,
            NeverDrop = dto.NeverDrop,
            _source = dto,
        };
    }

    internal static IList<GradingRules> From(ICanvasClient client, IEnumerable<GradingRulesDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}