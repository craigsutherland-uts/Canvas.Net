namespace Canvas.Client.Entities;
/// <summary>
/// The Rubric rating entity definition.
/// </summary>
public sealed partial record RubricRating
{
    private RubricRatingDto? _source;
    private readonly ICanvasClient _client;
    internal RubricRating(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the RubricRating is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Full marks&quot;
    /// </example>
    public string? Description { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;rat1&quot;
    /// </example>
    public string? Id { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Student completed the assignment flawlessly.&quot;
    /// </example>
    public string? LongDescription { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 10
    /// </example>
    public int? Points { get; init; }

    internal static RubricRating? From(ICanvasClient client, RubricRatingDto? dto)
    {
        if (dto == null)
            return null;
        return new RubricRating(client)
        {
            Description = dto.Description,
            Id = dto.Id,
            LongDescription = dto.LongDescription,
            Points = dto.Points,
            _source = dto,
        };
    }

    internal static IList<RubricRating> From(ICanvasClient client, IEnumerable<RubricRatingDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}