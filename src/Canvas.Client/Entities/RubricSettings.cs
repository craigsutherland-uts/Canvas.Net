namespace Canvas.Client.Entities;
/// <summary>
/// The Rubric settings entity definition.
/// </summary>
public sealed partial record RubricSettings
{
    private RubricSettingsDto? _source;
    /// <summary>
    /// A flag indicating whether the RubricSettings is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 123
    /// </example>
    public int? Id { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Coursework&quot;
    /// </example>
    public string? Title { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 123.12
    /// </example>
    public double? PointsPossible { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? FreeFormCriterionComments { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? HideScoreTotal { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? HidePoints { get; init; }

    internal static RubricSettings? From(RubricSettingsDto? dto)
    {
        if (dto == null)
            return null;
        return new RubricSettings
        {
            Id = dto.Id,
            Title = dto.Title,
            PointsPossible = dto.PointsPossible,
            FreeFormCriterionComments = dto.FreeFormCriterionComments,
            HideScoreTotal = dto.HideScoreTotal,
            HidePoints = dto.HidePoints,
            _source = dto,
        };
    }

    internal static IList<RubricSettings> From(IEnumerable<RubricSettingsDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(item)!)];
    }
}