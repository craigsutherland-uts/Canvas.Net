using Canvas.Client.Dtos;

namespace Canvas.Client.Entities;
/// <summary>
/// The Course progress entity definition.
/// </summary>
public sealed partial record CourseProgress
{
    private CourseProgressDto? _source;
    /// <summary>
    /// A flag indicating whether the CourseProgress is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// date the course was completed. null if the course has not been completed by this user
    /// </summary> 
    /// <example>
    /// 2013-06-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? CompletedAt { get; init; }
    /// <summary>
    /// url to next module item that has an unmet requirement. null if the user has completed the course or the current module does not require sequential progress
    /// </summary> 
    /// <example>
    /// "http://localhost/courses/1/modules/items/2"
    /// </example>
    public string? NextRequirementUrl { get; init; }
    /// <summary>
    /// total number of requirements the user has completed from all modules
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? RequirementCompletedCount { get; init; }
    /// <summary>
    /// total number of requirements from all modules
    /// </summary> 
    /// <example>
    /// 10
    /// </example>
    public int? RequirementCount { get; init; }

    internal static CourseProgress? From(CourseProgressDto? dto)
    {
        if (dto == null)
            return null;
        return new CourseProgress
        {
            CompletedAt = dto.CompletedAt,
            NextRequirementUrl = dto.NextRequirementUrl,
            RequirementCompletedCount = dto.RequirementCompletedCount,
            RequirementCount = dto.RequirementCount,
            _source = dto,
        };
    }

    internal static IList<CourseProgress> From(IEnumerable<CourseProgressDto>? dto)
    {
        if (dto == null)
            return [];
        return [.. dto.Select(item => From(item)!)];
    }
}