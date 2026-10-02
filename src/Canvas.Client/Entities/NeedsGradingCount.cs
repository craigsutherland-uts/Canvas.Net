namespace Canvas.Client.Entities;
/// <summary>
/// The Needs grading count entity definition.
/// </summary>
public sealed partial record NeedsGradingCount
{
    private NeedsGradingCountDto? _source;
    /// <summary>
    /// A flag indicating whether the NeedsGradingCount is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// Number of submissions that need grading
    /// </summary> 
    /// <example>
    /// 5
    /// </example>
    public int? Count { get; init; }
    /// <summary>
    /// The section ID
    /// </summary> 
    /// <example>
    /// &quot;123456&quot;
    /// </example>
    public string? SectionId { get; init; }

    internal static NeedsGradingCount? From(NeedsGradingCountDto? dto)
    {
        if (dto == null)
            return null;
        return new NeedsGradingCount
        {
            Count = dto.Count,
            SectionId = dto.SectionId,
            _source = dto,
        };
    }

    internal static IList<NeedsGradingCount> From(IEnumerable<NeedsGradingCountDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(item)!)];
    }
}