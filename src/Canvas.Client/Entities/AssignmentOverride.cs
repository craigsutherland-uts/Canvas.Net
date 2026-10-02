namespace Canvas.Client.Entities;
/// <summary>
/// The Assignment override entity definition.
/// </summary>
public sealed partial record AssignmentOverride
{
    private AssignmentOverrideDto? _source;
    private readonly ICanvasClient _client;
    internal AssignmentOverride(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the AssignmentOverride is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// the overridden all day flag (present if due_at is overridden)
    /// </summary>
    public int? AllDay { get; init; }
    /// <summary>
    /// the overridden all day date (present if due_at is overridden)
    /// </summary>
    public DateTime? AllDayDate { get; init; }
    /// <summary>
    /// the ID of the assignment the override applies to
    /// </summary> 
    /// <example>
    /// 123
    /// </example>
    public int? AssignmentId { get; init; }
    /// <summary>
    /// the ID of the overrides&apos;s target section (present if the override targets a section)
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? CourseSectionId { get; init; }
    /// <summary>
    /// the overridden due at (present if due_at is overridden)
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? DueAt { get; init; }
    /// <summary>
    /// the ID of the override&apos;s target group (present if the override targets a group and the assignment is a group assignment)
    /// </summary> 
    /// <example>
    /// 2
    /// </example>
    public int? GroupId { get; init; }
    /// <summary>
    /// the ID of the assignment override
    /// </summary> 
    /// <example>
    /// 4
    /// </example>
    public int? Id { get; init; }
    /// <summary>
    /// the overridden lock at, if any (present if lock_at is overridden)
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? LockAt { get; init; }
    /// <summary>
    /// the IDs of the override&apos;s target students (present if the override targets an ad-hoc set of students)
    /// </summary> 
    /// <example>
    /// [1, 2, 3]
    /// </example>
    public IList<int>? StudentIds { get; init; }
    /// <summary>
    /// the title of the override
    /// </summary> 
    /// <example>
    /// &quot;an assignment override&quot;
    /// </example>
    public string? Title { get; init; }
    /// <summary>
    /// the overridden unlock at (present if unlock_at is overridden)
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? UnlockAt { get; init; }

    internal static AssignmentOverride? From(ICanvasClient client, AssignmentOverrideDto? dto)
    {
        if (dto == null)
            return null;
        return new AssignmentOverride(client)
        {
            AllDay = dto.AllDay,
            AllDayDate = dto.AllDayDate,
            AssignmentId = dto.AssignmentId,
            CourseSectionId = dto.CourseSectionId,
            DueAt = dto.DueAt,
            GroupId = dto.GroupId,
            Id = dto.Id,
            LockAt = dto.LockAt,
            StudentIds = dto.StudentIds,
            Title = dto.Title,
            UnlockAt = dto.UnlockAt,
            _source = dto,
        };
    }

    internal static IList<AssignmentOverride> From(ICanvasClient client, IEnumerable<AssignmentOverrideDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}