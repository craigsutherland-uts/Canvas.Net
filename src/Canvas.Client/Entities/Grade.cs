namespace Canvas.Client.Entities;
/// <summary>
/// The Grade entity definition.
/// </summary>
public sealed partial record Grade
{
    private GradeDto? _source;
    private readonly ICanvasClient _client;
    internal Grade(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the Grade is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// The user&apos;s current grade in the class. Only included if user has permissions to view this grade.
    /// </summary>
    public string? CurrentGrade { get; init; }
    /// <summary>
    /// The user&apos;s current score in the class. Only included if user has permissions to view this score.
    /// </summary>
    public double? CurrentScore { get; init; }
    /// <summary>
    /// The user&apos;s final grade for the class. Only included if user has permissions to view this grade.
    /// </summary>
    public string? FinalGrade { get; init; }
    /// <summary>
    /// The user&apos;s final score for the class. Only included if user has permissions to view this score.
    /// </summary>
    public double? FinalScore { get; init; }
    /// <summary>
    /// The URL to the Canvas web UI page for the user&apos;s grades, if this is a student enrollment.
    /// </summary>
    public string? HtmlUrl { get; init; }
    /// <summary>
    /// The user&apos;s current grade in the class including muted/unposted assignments. Only included if user has permissions to view this grade, typically teachers, TAs, and admins.
    /// </summary>
    public string? UnpostedCurrentGrade { get; init; }
    /// <summary>
    /// The user&apos;s current score in the class including muted/unposted assignments. Only included if user has permissions to view this score, typically teachers, TAs, and admins..
    /// </summary>
    public double? UnpostedCurrentScore { get; init; }
    /// <summary>
    /// The user&apos;s final grade for the class including muted/unposted assignments. Only included if user has permissions to view this grade, typically teachers, TAs, and admins..
    /// </summary>
    public string? UnpostedFinalGrade { get; init; }
    /// <summary>
    /// The user&apos;s final score for the class including muted/unposted assignments. Only included if user has permissions to view this score, typically teachers, TAs, and admins..
    /// </summary>
    public double? UnpostedFinalScore { get; init; }

    internal static Grade? From(ICanvasClient client, GradeDto? dto)
    {
        if (dto == null)
            return null;
        return new Grade(client)
        {
            CurrentGrade = dto.CurrentGrade,
            CurrentScore = dto.CurrentScore,
            FinalGrade = dto.FinalGrade,
            FinalScore = dto.FinalScore,
            HtmlUrl = dto.HtmlUrl,
            UnpostedCurrentGrade = dto.UnpostedCurrentGrade,
            UnpostedCurrentScore = dto.UnpostedCurrentScore,
            UnpostedFinalGrade = dto.UnpostedFinalGrade,
            UnpostedFinalScore = dto.UnpostedFinalScore,
            _source = dto,
        };
    }

    internal static IList<Grade> From(ICanvasClient client, IEnumerable<GradeDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}