namespace Canvas.Client.Entities;
/// <summary>
/// The Grading period entity definition.
/// </summary>
public sealed partial record GradingPeriod
{
    private GradingPeriodDto? _source;
    private readonly ICanvasClient _client;
    internal GradingPeriod(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the GradingPeriod is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// Grades can only be changed before the close date of the grading period.
    /// </summary> 
    /// <example>
    /// 2013-02-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? CloseDate { get; init; }
    /// <summary>
    /// The end date of the grading period.
    /// </summary> 
    /// <example>
    /// 2013-02-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? EndDate { get; init; }
    /// <summary>
    /// The unique identifier for the grading period.
    /// </summary>
    public int? Id { get; init; }
    /// <summary>
    /// If true, the grading period&apos;s close_date has passed.
    /// </summary>
    public bool? IsClosed { get; init; }
    /// <summary>
    /// The start date of the grading period.
    /// </summary> 
    /// <example>
    /// 2013-02-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? StartDate { get; init; }
    /// <summary>
    /// The title for the grading period.
    /// </summary>
    public string? Title { get; init; }
    /// <summary>
    /// A weight value that contributes to the overall weight of a grading period set which is used to calculate how much assignments in this period contribute to the total grade.
    /// </summary>
    public double? Weight { get; init; }

    internal static GradingPeriod? From(ICanvasClient client, GradingPeriodDto? dto)
    {
        if (dto == null)
            return null;
        return new GradingPeriod(client)
        {
            CloseDate = dto.CloseDate,
            EndDate = dto.EndDate,
            Id = dto.Id,
            IsClosed = dto.IsClosed,
            StartDate = dto.StartDate,
            Title = dto.Title,
            Weight = dto.Weight,
            _source = dto,
        };
    }

    internal static IList<GradingPeriod> From(ICanvasClient client, IEnumerable<GradingPeriodDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}