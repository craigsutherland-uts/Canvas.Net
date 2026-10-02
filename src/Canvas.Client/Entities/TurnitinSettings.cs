namespace Canvas.Client.Entities;
/// <summary>
/// The Turnitin settings entity definition.
/// </summary>
public sealed partial record TurnitinSettings
{
    private TurnitinSettingsDto? _source;
    /// <summary>
    /// A flag indicating whether the TurnitinSettings is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? ExcludeBiblio { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? ExcludeQuoted { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;percent&quot;
    /// </example>
    public string? ExcludeSmallMatchesType { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 50
    /// </example>
    public int? ExcludeSmallMatchesValue { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? InternetCheck { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? JournalCheck { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;after_grading&quot;
    /// </example>
    public string? OriginalityReportVisibility { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? SPaperCheck { get; init; }

    internal static TurnitinSettings? From(TurnitinSettingsDto? dto)
    {
        if (dto == null)
            return null;
        return new TurnitinSettings
        {
            ExcludeBiblio = dto.ExcludeBiblio,
            ExcludeQuoted = dto.ExcludeQuoted,
            ExcludeSmallMatchesType = dto.ExcludeSmallMatchesType,
            ExcludeSmallMatchesValue = dto.ExcludeSmallMatchesValue,
            InternetCheck = dto.InternetCheck,
            JournalCheck = dto.JournalCheck,
            OriginalityReportVisibility = dto.OriginalityReportVisibility,
            SPaperCheck = dto.SPaperCheck,
            _source = dto,
        };
    }

    internal static IList<TurnitinSettings> From(IEnumerable<TurnitinSettingsDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(item)!)];
    }
}