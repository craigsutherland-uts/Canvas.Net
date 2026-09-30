using Canvas.Client.Dtos;

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
    /// <example>
    /// False
    /// </example>
    public bool? ExcludeBiblio { get; init; }
    /// <example>
    /// False
    /// </example>
    public bool? ExcludeQuoted { get; init; }
    /// <example>
    /// "percent"
    /// </example>
    public string? ExcludeSmallMatchesType { get; init; }
    /// <example>
    /// 50
    /// </example>
    public int? ExcludeSmallMatchesValue { get; init; }
    /// <example>
    /// False
    /// </example>
    public bool? InternetCheck { get; init; }
    /// <example>
    /// False
    /// </example>
    public bool? JournalCheck { get; init; }
    /// <example>
    /// "after_grading"
    /// </example>
    public string? OriginalityReportVisibility { get; init; }
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
}