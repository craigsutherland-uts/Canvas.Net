namespace Canvas.Client.Entities;
/// <summary>
/// The Course entity definition.
/// </summary>
public sealed partial record Course
{
    private CourseDto? _source;
    private readonly ICourses _client;
    internal Course(ICourses client)
    {
        Guard.IsNotNull(client);
        _client = client;
    }

    /// <summary>
    /// A flag indicating whether the Course is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// optional: this will be true if this user is currently prevented from viewing the course because of date restriction settings
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? AccessRestrictedByDate { get; init; }
    /// <summary>
    /// the account associated with the course
    /// </summary> 
    /// <example>
    /// 81259
    /// </example>
    public AccountIdentifier? AccountId { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? AllowStudentAssignmentEdits { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? AllowStudentForumAttachments { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? AllowWikiComments { get; init; }
    /// <summary>
    /// weight final grade based on assignment group percentages
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? ApplyAssignmentGroupWeights { get; init; }
    /// <summary>
    /// optional: whether the course is set as a Blueprint Course (blueprint fields require the Blueprint Courses feature)
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? Blueprint { get; init; }
    /// <summary>
    /// optional: Set of restrictions applied to all locked course objects
    /// </summary>
    public object? BlueprintRestrictions { get; init; }
    /// <summary>
    /// optional: Sets of restrictions differentiated by object type applied to locked course objects
    /// </summary>
    public object? BlueprintRestrictionsByObjectType { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public CalendarLink? Calendar { get; init; }
    /// <summary>
    /// the course code
    /// </summary> 
    /// <example>
    /// &quot;INSTCON12&quot;
    /// </example>
    public string? CourseCode { get; init; }
    /// <summary>
    /// Not specified in Canvas API spec, I&apos;m just guessing the type here
    /// </summary>
    public string? CourseColor { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;online&quot;
    /// </example>
    public string? CourseFormat { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public CourseProgress? CourseProgress { get; init; }
    /// <summary>
    /// the date the course was created.
    /// </summary> 
    /// <example>
    /// 2012-05-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? CreatedAt { get; init; }
    /// <summary>
    /// the type of page that users will see when they first visit the course - &apos;feed&apos;: Recent Activity Dashboard - &apos;wiki&apos;: Wiki Front Page - &apos;modules&apos;: Course Modules/Sections Page - &apos;assignments&apos;: Course Assignments List - &apos;syllabus&apos;: Course Syllabus Page other types may be added in the future
    /// </summary> 
    /// <example>
    /// &quot;feed&quot;
    /// </example>
    public string? DefaultView { get; init; }
    /// <summary>
    /// the end date for the course, if applicable
    /// </summary> 
    /// <example>
    /// 2012-09-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? EndAt { get; init; }
    /// <summary>
    /// the enrollment term associated with the course
    /// </summary> 
    /// <example>
    /// 34
    /// </example>
    public TermIdentifier? EnrollmentTermId { get; init; }
    /// <summary>
    /// A list of enrollments linking the current user to the course. for student enrollments, grading information may be included if include[]=total_scores
    /// </summary>
    public IList<Enrollment>? Enrollments { get; init; }
    /// <summary>
    /// Not specified in Canvas API spec, I&apos;m just guessing the type here
    /// </summary>
    public string? FriendlyName { get; init; }
    /// <summary>
    /// The grade_passback_setting on this course
    /// </summary> 
    /// <example>
    /// &quot;nightly_sync&quot;
    /// </example>
    public string? GradePassbackSetting { get; init; }
    /// <summary>
    /// A list of grading periods associated with the course
    /// </summary>
    public IList<GradingPeriod>? GradingPeriods { get; init; }
    /// <summary>
    /// the grading standard associated with the course
    /// </summary> 
    /// <example>
    /// 25
    /// </example>
    public int? GradingStandardId { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? HideFinalGrades { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? HomeroomCourse { get; init; }
    /// <summary>
    /// the unique identifier for the course
    /// </summary> 
    /// <example>
    /// 370663
    /// </example>
    public CourseIdentifier Id { get; init; }
    /// <summary>
    /// the integration identifier for the course, if defined. This field is only included if the user has permission to view SIS information.
    /// </summary>
    public string? IntegrationId { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? IsPublic { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? IsPublicToAuthUsers { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;Creative Commons&quot;
    /// </example>
    public string? License { get; init; }
    /// <summary>
    /// the course-set locale, if applicable
    /// </summary> 
    /// <example>
    /// &quot;en&quot;
    /// </example>
    public string? Locale { get; init; }
    /// <summary>
    /// the full name of the course
    /// </summary> 
    /// <example>
    /// &quot;InstructureCon 2012&quot;
    /// </example>
    public string? Name { get; init; }
    /// <summary>
    /// optional: the number of submissions needing grading returned only if the current user has grading rights and include[]=needs_grading_count
    /// </summary> 
    /// <example>
    /// 17
    /// </example>
    public int? NeedsGradingCount { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? OpenEnrollment { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// &quot;INSTCON12&quot;
    /// </example>
    public string? OriginalName { get; init; }
    /// <summary>
    /// optional: the permissions the user has for the course. returned only for a single course and include[]=permissions
    /// </summary>
    public object? Permissions { get; init; }
    /// <summary>
    /// optional: the public description of the course
    /// </summary> 
    /// <example>
    /// &quot;Come one, come all to InstructureCon 2012!&quot;
    /// </example>
    public string? PublicDescription { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? PublicSyllabus { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? PublicSyllabusToAuth { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? RestrictEnrollmentsToCourseDates { get; init; }
    /// <summary>
    /// the root account associated with the course
    /// </summary> 
    /// <example>
    /// 81259
    /// </example>
    public AccountIdentifier? RootAccountId { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? SelfEnrollment { get; init; }
    /// <summary>
    /// the SIS identifier for the course, if defined. This field is only included if the user has permission to view SIS information.
    /// </summary>
    public string? SisCourseId { get; init; }
    /// <summary>
    /// the unique identifier for the SIS import. This field is only included if the user has permission to manage SIS information.
    /// </summary> 
    /// <example>
    /// 34
    /// </example>
    public  string ? SisImportId { get; init; }
    /// <summary>
    /// the start date for the course, if applicable
    /// </summary> 
    /// <example>
    /// 2012-06-01T00:00:00.0000000-06:00
    /// </example>
    public DateTime? StartAt { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 5
    /// </example>
    public int? StorageQuotaMb { get; init; }
    /// <summary>
    /// 
    /// </summary> 
    /// <example>
    /// 5
    /// </example>
    public double? StorageQuotaUsedMb { get; init; }
    /// <summary>
    /// optional: user-generated HTML for the course syllabus
    /// </summary> 
    /// <example>
    /// &quot;&lt;p&gt;syllabus html goes here&lt;/p&gt;&quot;
    /// </example>
    public string? SyllabusBody { get; init; }
    /// <summary>
    /// optional: whether the course is set as a template (requires the course templates feature)
    /// </summary>
    public bool? Template { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public Term? Term { get; init; }
    /// <summary>
    /// The course&apos;s IANA time zone name.
    /// </summary> 
    /// <example>
    /// &quot;America/Denver&quot;
    /// </example>
    public string? TimeZone { get; init; }
    /// <summary>
    /// optional: the total number of active and invited students in the course
    /// </summary> 
    /// <example>
    /// 32
    /// </example>
    public int? TotalStudents { get; init; }
    /// <summary>
    /// the UUID of the course
    /// </summary> 
    /// <example>
    /// &quot;WvAHhY5FINzq5IyRIJybGeiXyFkG3SqHUPb7jZY5&quot;
    /// </example>
    public string? Uuid { get; init; }
    /// <summary>
    /// the current state of the course one of &apos;unpublished&apos;, &apos;available&apos;, &apos;completed&apos;, or &apos;deleted&apos;
    /// </summary> 
    /// <example>
    /// &quot;available&quot;
    /// </example>
    public string? WorkflowState { get; init; }

    internal static Course? From(ICourses client, CourseDto? dto)
    {
        if (dto == null)
            return null;
        return new Course(client)
        {
            AccessRestrictedByDate = dto.AccessRestrictedByDate,
            AccountId = AccountIdentifier.FromNullable(dto.AccountId),
            AllowStudentAssignmentEdits = dto.AllowStudentAssignmentEdits,
            AllowStudentForumAttachments = dto.AllowStudentForumAttachments,
            AllowWikiComments = dto.AllowWikiComments,
            ApplyAssignmentGroupWeights = dto.ApplyAssignmentGroupWeights,
            Blueprint = dto.Blueprint,
            BlueprintRestrictions = dto.BlueprintRestrictions,
            BlueprintRestrictionsByObjectType = dto.BlueprintRestrictionsByObjectType,
            Calendar = CalendarLink.From(dto.Calendar),
            CourseCode = dto.CourseCode,
            CourseColor = dto.CourseColor,
            CourseFormat = dto.CourseFormat,
            CourseProgress = CourseProgress.From(dto.CourseProgress),
            CreatedAt = dto.CreatedAt,
            DefaultView = dto.DefaultView,
            EndAt = dto.EndAt,
            EnrollmentTermId = TermIdentifier.FromNullable(dto.EnrollmentTermId),
            Enrollments = Enrollment.From(dto.Enrollments),
            FriendlyName = dto.FriendlyName,
            GradePassbackSetting = dto.GradePassbackSetting,
            GradingPeriods = GradingPeriod.From(dto.GradingPeriods),
            GradingStandardId = dto.GradingStandardId,
            HideFinalGrades = dto.HideFinalGrades,
            HomeroomCourse = dto.HomeroomCourse,
            Id = dto.Id == null ? CourseIdentifier.None : CourseIdentifier.From(dto.Id),
            IntegrationId = dto.IntegrationId,
            IsPublic = dto.IsPublic,
            IsPublicToAuthUsers = dto.IsPublicToAuthUsers,
            License = dto.License,
            Locale = dto.Locale,
            Name = dto.Name,
            NeedsGradingCount = dto.NeedsGradingCount,
            OpenEnrollment = dto.OpenEnrollment,
            OriginalName = dto.OriginalName,
            Permissions = dto.Permissions,
            PublicDescription = dto.PublicDescription,
            PublicSyllabus = dto.PublicSyllabus,
            PublicSyllabusToAuth = dto.PublicSyllabusToAuth,
            RestrictEnrollmentsToCourseDates = dto.RestrictEnrollmentsToCourseDates,
            RootAccountId = AccountIdentifier.FromNullable(dto.RootAccountId),
            SelfEnrollment = dto.SelfEnrollment,
            SisCourseId = dto.SisCourseId,
            SisImportId = dto.SisImportId,
            StartAt = dto.StartAt,
            StorageQuotaMb = dto.StorageQuotaMb,
            StorageQuotaUsedMb = dto.StorageQuotaUsedMb,
            SyllabusBody = dto.SyllabusBody,
            Template = dto.Template,
            Term = Term.From(dto.Term),
            TimeZone = dto.TimeZone,
            TotalStudents = dto.TotalStudents,
            Uuid = dto.Uuid,
            WorkflowState = dto.WorkflowState,
            _source = dto,
        };
    }

    internal static IList<Course> From(ICourses client, IEnumerable<CourseDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}