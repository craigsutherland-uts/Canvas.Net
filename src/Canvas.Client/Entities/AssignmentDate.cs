namespace Canvas.Client.Entities;
/// <summary>
/// The Assignment date entity definition.
/// </summary>
public sealed partial record AssignmentDate
{
    private AssignmentDateDto? _source;
    private readonly ICanvasClient _client;
    internal AssignmentDate(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the AssignmentDate is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// (Optional, present if &apos;id&apos; is missing) whether this date represents the assignment&apos;s or quiz&apos;s default due date
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? Base { get; init; }
    /// <summary>
    /// The due date for the assignment. Must be between the unlock date and the lock date if there are lock dates
    /// </summary> 
    /// <example>
    /// 2013-08-28T23:59:00.0000000-06:00
    /// </example>
    public DateTime? DueAt { get; init; }
    /// <summary>
    /// (Optional, missing if &apos;base&apos; is present) id of the assignment override this date represents
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? Id { get; init; }
    /// <summary>
    /// The lock date for the assignment. Must be after the due date if there is a due date.
    /// </summary> 
    /// <example>
    /// 2013-08-31T23:59:00.0000000-06:00
    /// </example>
    public DateTime? LockAt { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Summer Session&quot;
    /// </example>
    public string? Title { get; init; }
    /// <summary>
    /// The unlock date for the assignment. Must be before the due date if there is a due date.
    /// </summary> 
    /// <example>
    /// 2013-08-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? UnlockAt { get; init; }

    internal static AssignmentDate? From(ICanvasClient client, AssignmentDateDto? dto)
    {
        if (dto == null)
            return null;
        return new AssignmentDate(client)
        {
            Base = dto.Base,
            DueAt = dto.DueAt,
            Id = dto.Id,
            LockAt = dto.LockAt,
            Title = dto.Title,
            UnlockAt = dto.UnlockAt,
            _source = dto,
        };
    }

    internal static IList<AssignmentDate> From(ICanvasClient client, IEnumerable<AssignmentDateDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}