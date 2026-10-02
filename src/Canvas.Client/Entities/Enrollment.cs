namespace Canvas.Client.Entities;
/// <summary>
/// The Enrollment entity definition.
/// </summary>
public sealed partial record Enrollment
{
    private EnrollmentDto? _source;
    private readonly ICanvasClient _client;
    internal Enrollment(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the Enrollment is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// The unique id of the associated user. Will be null unless type is ObserverEnrollment.
    /// </summary>
    public int? AssociatedUserId { get; init; }
    /// <summary>
    /// optional: The letter grade equivalent of computed_current_score, if available. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;A-&quot;
    /// </example>
    public string? ComputedCurrentGrade { get; init; }
    /// <summary>
    /// optional: The student&apos;s score in the course, ignoring ungraded assignments. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 90.25
    /// </example>
    public double? ComputedCurrentScore { get; init; }
    /// <summary>
    /// optional: The letter grade equivalent of computed_final_score, if available. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;B-&quot;
    /// </example>
    public string? ComputedFinalGrade { get; init; }
    /// <summary>
    /// optional: The student&apos;s score in the course including ungraded assignments with a score of 0. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 80.67
    /// </example>
    public double? ComputedFinalScore { get; init; }
    /// <summary>
    /// The unique id of the course.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? CourseId { get; init; }
    /// <summary>
    /// The Course Integration ID in which the enrollment is associated. This field is only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? CourseIntegrationId { get; init; }
    /// <summary>
    /// The unique id of the user&apos;s section.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? CourseSectionId { get; init; }
    /// <summary>
    /// The created time of the enrollment, in ISO8601 format.
    /// </summary> 
    /// <example>
    /// 2012-04-18T23:08:51.0000000+00:00
    /// </example>
    public DateTime? CreatedAt { get; init; }
    /// <summary>
    /// optional: The id of the currently active grading period, if one exists. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 5
    /// </example>
    public int? CurrentGradingPeriodId { get; init; }
    /// <summary>
    /// optional: The name of the currently active grading period, if one exists. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;Fall Grading Period&quot;
    /// </example>
    public string? CurrentGradingPeriodTitle { get; init; }
    /// <summary>
    /// optional: The letter grade equivalent of current_period_computed_current_score, if available. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;A&quot;
    /// </example>
    public string? CurrentPeriodComputedCurrentGrade { get; init; }
    /// <summary>
    /// optional: The student&apos;s score in the course for the current grading period, ignoring ungraded assignments. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 95.8
    /// </example>
    public double? CurrentPeriodComputedCurrentScore { get; init; }
    /// <summary>
    /// optional: The letter grade equivalent of current_period_computed_final_score, if available. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;B&quot;
    /// </example>
    public string? CurrentPeriodComputedFinalGrade { get; init; }
    /// <summary>
    /// optional: The student&apos;s score in the course for the current grading period, including ungraded assignments with a score of 0. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 85.25
    /// </example>
    public double? CurrentPeriodComputedFinalScore { get; init; }
    /// <summary>
    /// The user&apos;s override grade for the current grading period.
    /// </summary>
    public string? CurrentPeriodOverrideGrade { get; init; }
    /// <summary>
    /// The user&apos;s override score for the current grading period.
    /// </summary>
    public double? CurrentPeriodOverrideScore { get; init; }
    /// <summary>
    /// optional: The letter grade equivalent of current_period_unposted_current_score, if available. Only included if user has permission to view this grade, typically teachers, TAs, and admins. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;A&quot;
    /// </example>
    public string? CurrentPeriodUnpostedCurrentGrade { get; init; }
    /// <summary>
    /// optional: The student&apos;s score in the course for the current grading period, including muted/unposted assignments. Only included if user has permission to view this score, typically teachers, TAs, and admins. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 95.8
    /// </example>
    public double? CurrentPeriodUnpostedCurrentScore { get; init; }
    /// <summary>
    /// optional: The letter grade equivalent of current_period_unposted_final_score, if available. Only included if user has permission to view this grade, typically teachers, TAs, and admins. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// &quot;B&quot;
    /// </example>
    public string? CurrentPeriodUnpostedFinalGrade { get; init; }
    /// <summary>
    /// optional: The student&apos;s score in the course for the current grading period, including muted/unposted assignments and including ungraded assignments with a score of 0. Only included if user has permission to view this score, typically teachers, TAs, and admins. If the course the enrollment belongs to does not have grading periods, or if no currently active grading period exists, the value will be null. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// 85.25
    /// </example>
    public double? CurrentPeriodUnpostedFinalScore { get; init; }
    /// <summary>
    /// The end time of the enrollment, in ISO8601 format.
    /// </summary> 
    /// <example>
    /// 2012-04-18T23:08:51.0000000+00:00
    /// </example>
    public DateTime? EndAt { get; init; }
    /// <summary>
    /// The state of the user&apos;s enrollment in the course.
    /// </summary> 
    /// <example>
    /// &quot;active&quot;
    /// </example>
    public string? EnrollmentState { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public Grade? Grades { get; init; }
    /// <summary>
    /// optional: Indicates whether the course the enrollment belongs to has grading periods set up. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? HasGradingPeriods { get; init; }
    /// <summary>
    /// The URL to the Canvas web UI page for this course enrollment.
    /// </summary> 
    /// <example>
    /// &quot;https://...&quot;
    /// </example>
    public string? HtmlUrl { get; init; }
    /// <summary>
    /// The ID of the enrollment.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? Id { get; init; }
    /// <summary>
    /// The last activity time of the user for the enrollment, in ISO8601 format.
    /// </summary> 
    /// <example>
    /// 2012-04-18T23:08:51.0000000+00:00
    /// </example>
    public DateTime? LastActivityAt { get; init; }
    /// <summary>
    /// The last attended date of the user for the enrollment in a course, in ISO8601 format.
    /// </summary> 
    /// <example>
    /// 2012-04-18T23:08:51.0000000+00:00
    /// </example>
    public DateTime? LastAttendedAt { get; init; }
    /// <summary>
    /// User can only access his or her own course section.
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? LimitPrivilegesToCourseSection { get; init; }
    /// <summary>
    /// The user&apos;s override grade for the course.
    /// </summary>
    public string? OverrideGrade { get; init; }
    /// <summary>
    /// The user&apos;s override score for the course.
    /// </summary>
    public double? OverrideScore { get; init; }
    /// <summary>
    /// The enrollment role, for course-level permissions. This field will match `type` if the enrollment role has not been customized.
    /// </summary> 
    /// <example>
    /// &quot;StudentEnrollment&quot;
    /// </example>
    public string? Role { get; init; }
    /// <summary>
    /// The id of the enrollment role.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public RoleIdentifier? RoleId { get; init; }
    /// <summary>
    /// The unique id of the user&apos;s account.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? RootAccountId { get; init; }
    /// <summary>
    /// The Section Integration ID in which the enrollment is associated. This field is only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? SectionIntegrationId { get; init; }
    /// <summary>
    /// The SIS Account ID in which the enrollment is associated. Only displayed if present. This field is only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? SisAccountId { get; init; }
    /// <summary>
    /// The SIS Course ID in which the enrollment is associated. Only displayed if present. This field is only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? SisCourseId { get; init; }
    /// <summary>
    /// The unique identifier for the SIS import. This field is only included if the user has permission to manage SIS information.
    /// </summary> 
    /// <example>
    /// 83
    /// </example>
    public int? SisImportId { get; init; }
    /// <summary>
    /// The SIS Section ID in which the enrollment is associated. Only displayed if present. This field is only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? SisSectionId { get; init; }
    /// <summary>
    /// The SIS User ID in which the enrollment is associated. Only displayed if present. This field is only included if the user has permission to view SIS information.
    /// </summary> 
    /// <example>
    /// &quot;SHEL93921&quot;
    /// </example>
    public string? SisUserId { get; init; }
    /// <summary>
    /// The start time of the enrollment, in ISO8601 format.
    /// </summary> 
    /// <example>
    /// 2012-04-18T23:08:51.0000000+00:00
    /// </example>
    public DateTime? StartAt { get; init; }
    /// <summary>
    /// The total activity time of the user for the enrollment, in seconds.
    /// </summary> 
    /// <example>
    /// 260
    /// </example>
    public int? TotalActivityTime { get; init; }
    /// <summary>
    /// optional: Indicates whether the course the enrollment belongs to has the Display Totals for &apos;All Grading Periods&apos; feature enabled. (applies only to student enrollments, and only available in course endpoints)
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? TotalsForAllGradingPeriodsOption { get; init; }
    /// <summary>
    /// The enrollment type. One of &apos;StudentEnrollment&apos;, &apos;TeacherEnrollment&apos;, &apos;TaEnrollment&apos;, &apos;DesignerEnrollment&apos;, &apos;ObserverEnrollment&apos;.
    /// </summary> 
    /// <example>
    /// &quot;StudentEnrollment&quot;
    /// </example>
    public string? Type { get; init; }
    /// <summary>
    /// The user&apos;s current grade in the class including muted/unposted assignments. Only included if user has permissions to view this grade, typically teachers, TAs, and admins.
    /// </summary>
    public string? UnpostedCurrentGrade { get; init; }
    /// <summary>
    /// The user&apos;s current score in the class including muted/unposted assignments. Only included if user has permissions to view this score, typically teachers, TAs, and admins..
    /// </summary>
    public string? UnpostedCurrentScore { get; init; }
    /// <summary>
    /// The user&apos;s final grade for the class including muted/unposted assignments. Only included if user has permissions to view this grade, typically teachers, TAs, and admins..
    /// </summary>
    public string? UnpostedFinalGrade { get; init; }
    /// <summary>
    /// The user&apos;s final score for the class including muted/unposted assignments. Only included if user has permissions to view this score, typically teachers, TAs, and admins..
    /// </summary>
    public double? UnpostedFinalScore { get; init; }
    /// <summary>
    /// The updated time of the enrollment, in ISO8601 format.
    /// </summary> 
    /// <example>
    /// 2012-04-18T23:08:51.0000000+00:00
    /// </example>
    public DateTime? UpdatedAt { get; init; }
    /// <summary>
    /// A Canvas user, e.g. a student, teacher, administrator, observer, etc.
    /// </summary>
    public User? User { get; init; }
    /// <summary>
    /// The unique id of the user.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public UserIdentifier? UserId { get; init; }

    internal static Enrollment? From(ICanvasClient client, EnrollmentDto? dto)
    {
        if (dto == null)
            return null;
        return new Enrollment(client)
        {
            AssociatedUserId = dto.AssociatedUserId,
            ComputedCurrentGrade = dto.ComputedCurrentGrade,
            ComputedCurrentScore = dto.ComputedCurrentScore,
            ComputedFinalGrade = dto.ComputedFinalGrade,
            ComputedFinalScore = dto.ComputedFinalScore,
            CourseId = dto.CourseId,
            CourseIntegrationId = dto.CourseIntegrationId,
            CourseSectionId = dto.CourseSectionId,
            CreatedAt = dto.CreatedAt,
            CurrentGradingPeriodId = dto.CurrentGradingPeriodId,
            CurrentGradingPeriodTitle = dto.CurrentGradingPeriodTitle,
            CurrentPeriodComputedCurrentGrade = dto.CurrentPeriodComputedCurrentGrade,
            CurrentPeriodComputedCurrentScore = dto.CurrentPeriodComputedCurrentScore,
            CurrentPeriodComputedFinalGrade = dto.CurrentPeriodComputedFinalGrade,
            CurrentPeriodComputedFinalScore = dto.CurrentPeriodComputedFinalScore,
            CurrentPeriodOverrideGrade = dto.CurrentPeriodOverrideGrade,
            CurrentPeriodOverrideScore = dto.CurrentPeriodOverrideScore,
            CurrentPeriodUnpostedCurrentGrade = dto.CurrentPeriodUnpostedCurrentGrade,
            CurrentPeriodUnpostedCurrentScore = dto.CurrentPeriodUnpostedCurrentScore,
            CurrentPeriodUnpostedFinalGrade = dto.CurrentPeriodUnpostedFinalGrade,
            CurrentPeriodUnpostedFinalScore = dto.CurrentPeriodUnpostedFinalScore,
            EndAt = dto.EndAt,
            EnrollmentState = dto.EnrollmentState,
            Grades = Grade.From(client, dto.Grades),
            HasGradingPeriods = dto.HasGradingPeriods,
            HtmlUrl = dto.HtmlUrl,
            Id = dto.Id,
            LastActivityAt = dto.LastActivityAt,
            LastAttendedAt = dto.LastAttendedAt,
            LimitPrivilegesToCourseSection = dto.LimitPrivilegesToCourseSection,
            OverrideGrade = dto.OverrideGrade,
            OverrideScore = dto.OverrideScore,
            Role = dto.Role,
            RoleId = RoleIdentifier.FromNullable(dto.RoleId),
            RootAccountId = dto.RootAccountId,
            SectionIntegrationId = dto.SectionIntegrationId,
            SisAccountId = dto.SisAccountId,
            SisCourseId = dto.SisCourseId,
            SisImportId = dto.SisImportId,
            SisSectionId = dto.SisSectionId,
            SisUserId = dto.SisUserId,
            StartAt = dto.StartAt,
            TotalActivityTime = dto.TotalActivityTime,
            TotalsForAllGradingPeriodsOption = dto.TotalsForAllGradingPeriodsOption,
            Type = dto.Type,
            UnpostedCurrentGrade = dto.UnpostedCurrentGrade,
            UnpostedCurrentScore = dto.UnpostedCurrentScore,
            UnpostedFinalGrade = dto.UnpostedFinalGrade,
            UnpostedFinalScore = dto.UnpostedFinalScore,
            UpdatedAt = dto.UpdatedAt,
            User = User.From(client, dto.User),
            UserId = UserIdentifier.FromNullable(dto.UserId),
            _source = dto,
        };
    }

    internal static IList<Enrollment> From(ICanvasClient client, IEnumerable<EnrollmentDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}