using Canvas.Client.Dtos;

namespace Canvas.Client.Entities;

/// <summary>
/// The Lock info entity definition.
/// </summary>
public sealed partial record LockInfo
{
    private LockInfoDto? _source;
    /// <summary>
    /// A flag indicating whether the LockInfo is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// Asset string for the object causing the lock
    /// </summary> 
    /// <example>
    /// "assignment_4"
    /// </example>
    public string? AssetString { get; init; }
    /// <summary>
    /// (Optional) Context module causing the lock.
    /// </summary> 
    /// <example>
    /// "{}"
    /// </example>
    public string? ContextModule { get; init; }
    /// <summary>
    /// (Optional) Time at which this was/will be locked. Must be after the due date.
    /// </summary> 
    /// <example>
    /// 2013-02-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? LockAt { get; init; }
    /// <example>
    /// True
    /// </example>
    public bool? ManuallyLocked { get; init; }
    /// <summary>
    /// (Optional) Time at which this was/will be unlocked. Must be before the due date.
    /// </summary> 
    /// <example>
    /// 2013-01-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? UnlockAt { get; init; }

    internal static LockInfo? From(LockInfoDto? dto)
    {
        if (dto == null)
            return null;
        return new LockInfo
        {
            AssetString = dto.AssetString,
            ContextModule = dto.ContextModule,
            LockAt = dto.LockAt,
            ManuallyLocked = dto.ManuallyLocked,
            UnlockAt = dto.UnlockAt,
            _source = dto,
        };
    }
}