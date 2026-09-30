using Canvas.Client.Dtos;

namespace Canvas.Client.Entities;
/// <summary>
/// The Rubric criteria entity definition.
/// </summary>
public sealed partial record RubricCriteria
{
    private RubricCriteriaDto? _source;
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
    /// "Criterion 1"
    /// </example>
    public string? Description { get; init; }
    /// <summary>
    /// The id of rubric criteria.
    /// </summary> 
    /// <example>
    /// "crit1"
    /// </example>
    public string? Id { get; init; }
    /// <summary>
    /// (Optional) The id of the learning outcome this criteria uses, if any.
    /// </summary> 
    /// <example>
    /// "1234"
    /// </example>
    public string? LearningOutcomeId { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// "Criterion 1 more details"
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
    /// (Optional) The 3rd party vendor's GUID for the outcome this criteria references, if any.
    /// </summary> 
    /// <example>
    /// "abdsfjasdfne3jsdfn2"
    /// </example>
    public string? VendorGuid { get; init; }

    internal static RubricCriteria? From(RubricCriteriaDto? dto)
    {
        if (dto == null)
            return null;
        return new RubricCriteria
        {
            CriterionUseRange = dto.CriterionUseRange,
            Description = dto.Description,
            Id = dto.Id,
            LearningOutcomeId = dto.LearningOutcomeId,
            LongDescription = dto.LongDescription,
            Points = dto.Points,
            Ratings = RubricRating.From(dto.Ratings),
            VendorGuid = dto.VendorGuid,
            _source = dto,
        };
    }

    internal static IList<RubricCriteria> From(IEnumerable<RubricCriteriaDto>? dto)
    {
        if (dto == null)
            return [];
        return [.. dto.Select(item => From(item)!)];
    }
}