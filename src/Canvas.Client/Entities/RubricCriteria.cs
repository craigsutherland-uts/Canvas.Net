namespace Canvas.Client.Entities;
/// <summary>
/// The Rubric criteria entity definition.
/// </summary>
public sealed partial record RubricCriteria
{
    private RubricCriteriaDto? _source;
    private readonly ICanvasClient _client;
    internal RubricCriteria(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the RubricCriteria is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? CriterionUseRange { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Criterion 1&quot;
    /// </example>
    public string? Description { get; init; }
    /// <summary>
    /// The id of rubric criteria.
    /// </summary> 
    /// <example>
    /// &quot;crit1&quot;
    /// </example>
    public string? Id { get; init; }
    /// <summary>
    /// (Optional) The id of the learning outcome this criteria uses, if any.
    /// </summary> 
    /// <example>
    /// &quot;1234&quot;
    /// </example>
    public string? LearningOutcomeId { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Criterion 1 more details&quot;
    /// </example>
    public string? LongDescription { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 10
    /// </example>
    public int? Points { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public IList<RubricRating>? Ratings { get; init; }
    /// <summary>
    /// (Optional) The 3rd party vendor&apos;s GUID for the outcome this criteria references, if any.
    /// </summary> 
    /// <example>
    /// &quot;abdsfjasdfne3jsdfn2&quot;
    /// </example>
    public string? VendorGuid { get; init; }

    internal static RubricCriteria? From(ICanvasClient client, RubricCriteriaDto? dto)
    {
        if (dto == null)
            return null;
        return new RubricCriteria(client)
        {
            CriterionUseRange = dto.CriterionUseRange,
            Description = dto.Description,
            Id = dto.Id,
            LearningOutcomeId = dto.LearningOutcomeId,
            LongDescription = dto.LongDescription,
            Points = dto.Points,
            Ratings = RubricRating.From(client, dto.Ratings),
            VendorGuid = dto.VendorGuid,
            _source = dto,
        };
    }

    internal static IList<RubricCriteria> From(ICanvasClient client, IEnumerable<RubricCriteriaDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}