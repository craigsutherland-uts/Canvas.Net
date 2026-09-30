using Canvas.Client.Dtos;

namespace Canvas.Client.Entities;
/// <summary>
/// The Rubric rating entity definition.
/// </summary>
public sealed partial record RubricRating
{
    private RubricRatingDto? _source;
    /// <summary>
    /// A flag indicating whether the RubricRating is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// "Full marks"
    /// </example>
    public string? Description { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// "rat1"
    /// </example>
    public string? Id { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// "Student completed the assignment flawlessly."
    /// </example>
    public string? LongDescription { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 10
    /// </example>
    public int? Points { get; init; }

    internal static RubricRating? From(RubricRatingDto? dto)
    {
        if (dto == null)
            return null;
        return new RubricRating
        {
            Description = dto.Description,
            Id = dto.Id,
            LongDescription = dto.LongDescription,
            Points = dto.Points,
            _source = dto,
        };
    }

    internal static IList<RubricRating> From(IEnumerable<RubricRatingDto>? dto)
    {
        if (dto == null)
            return [];
        return [.. dto.Select(item => From(item)!)];
    }
}