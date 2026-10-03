namespace Canvas.Client.Entities;
/// <summary>
/// The Assignment entity definition.
/// </summary>
public sealed partial record Assignment
{
    private AssignmentDto? _source;
    private readonly ICanvasClient _client;
    internal Assignment(ICanvasClient client)
    {
        Guard.IsNotNull(client);
        _client = client;
        Initialise();
    }

    partial void Initialise();
    /// <summary>
    /// A flag indicating whether the Assignment is new or retrieved from Canvas.
    /// </summary>
    public bool IsNew => _source == null;
    /// <summary>
    /// (Optional) all dates associated with the assignment, if applicable
    /// </summary>
    public IList<AssignmentDate>? AllDates { get; init; }
    /// <summary>
    /// Allowed file extensions, which take effect if submission_types includes &apos;online_upload&apos;.
    /// </summary> 
    /// <example>
    /// [&quot;docx&quot;, &quot;ppt&quot;]
    /// </example>
    public IList<string>? AllowedExtensions { get; init; }
    /// <summary>
    /// (Optional) whether anonymous submissions are accepted (applies only to quiz assignments)
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? AnonymousSubmissions { get; init; }
    /// <summary>
    /// the ID of the assignment&apos;s group
    /// </summary> 
    /// <example>
    /// 2
    /// </example>
    public AssignmentGroupIdentifier? AssignmentGroupId { get; init; }
    /// <summary>
    /// (Optional) If &apos;assignment_visibility&apos; is included in the &apos;include&apos; parameter, includes an array of student IDs who can see this assignment.
    /// </summary> 
    /// <example>
    /// [137, 381, 572]
    /// </example>
    public IList<int>? AssignmentVisibility { get; init; }
    /// <summary>
    /// Boolean indicating peer reviews are assigned automatically. If false, the teacher is expected to manually assign peer reviews.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? AutomaticPeerReviews { get; init; }
    /// <summary>
    /// the ID of the course the assignment belongs to
    /// </summary> 
    /// <example>
    /// 123
    /// </example>
    public CourseIdentifier CourseId { get; init; }
    /// <summary>
    /// The time at which this assignment was originally created
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? CreatedAt { get; init; }
    /// <summary>
    /// the assignment description, in an HTML fragment
    /// </summary> 
    /// <example>
    /// &quot;&lt;p&gt;Do the following:&lt;/p&gt;...&quot;
    /// </example>
    public string? Description { get; init; }
    /// <summary>
    /// the due date for the assignment. returns null if not present. NOTE: If this assignment has assignment overrides, this field will be the due date as it applies to the user requesting information from the API.
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? DueAt { get; init; }
    /// <summary>
    /// Boolean flag indicating whether the assignment requires a due date based on the account level setting
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? DueDateRequired { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public ExternalToolTagAttributes? ExternalToolTagAttributes { get; init; }
    /// <summary>
    /// (Optional) Boolean indicating if assignment will be frozen when it is copied. NOTE: This field will only be present if the AssignmentFreezer plugin is available for your account.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? FreezeOnCopy { get; init; }
    /// <summary>
    /// (Optional) Boolean indicating if assignment is frozen for the calling user. NOTE: This field will only be present if the AssignmentFreezer plugin is available for your account.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? Frozen { get; init; }
    /// <summary>
    /// (Optional) Array of frozen attributes for the assignment. Only account administrators currently have permission to change an attribute in this list. Will be empty if no attributes are frozen for this assignment. Possible frozen attributes are: title, description, lock_at, points_possible, grading_type, submission_types, assignment_group_id, allowed_extensions, group_category_id, notify_of_update, peer_reviews NOTE: This field will only be present if the AssignmentFreezer plugin is available for your account.
    /// </summary> 
    /// <example>
    /// [&quot;title&quot;]
    /// </example>
    public IList<string>? FrozenAttributes { get; init; }
    /// <summary>
    /// If this is a group assignment, boolean flag indicating whether or not students will be graded individually.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? GradeGroupStudentsIndividually { get; init; }
    /// <summary>
    /// The id of the grading standard being applied to this assignment. Valid if grading_type is &apos;letter_grade&apos; or &apos;gpa_scale&apos;.
    /// </summary>
    public int? GradingStandardId { get; init; }
    /// <summary>
    /// The type of grading the assignment receives; one of &apos;pass_fail&apos;, &apos;percent&apos;, &apos;letter_grade&apos;, &apos;gpa_scale&apos;, &apos;points&apos;
    /// </summary> 
    /// <example>
    /// &quot;points&quot;
    /// </example>
    public string? GradingType { get; init; }
    /// <summary>
    /// The ID of the assignments group set, if this is a group assignment. For group discussions, set group_category_id on the discussion topic, not the linked assignment.
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? GroupCategoryId { get; init; }
    /// <summary>
    /// whether this assignment has overrides
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? HasOverrides { get; init; }
    /// <summary>
    /// If true, the assignment has been submitted to by at least one student
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? HasSubmittedSubmissions { get; init; }
    /// <summary>
    /// the URL to the assignment&apos;s web page
    /// </summary> 
    /// <example>
    /// &quot;https://...&quot;
    /// </example>
    public string? HtmlUrl { get; init; }
    /// <summary>
    /// the ID of the assignment
    /// </summary> 
    /// <example>
    /// 4
    /// </example>
    public AssignmentIdentifier Id { get; init; }
    /// <summary>
    /// (optional, Third Party integration data for assignment)
    /// </summary>
    public object? IntegrationData { get; init; }
    /// <summary>
    /// (optional, Third Party unique identifier for Assignment)
    /// </summary> 
    /// <example>
    /// &quot;12341234&quot;
    /// </example>
    public string? IntegrationId { get; init; }
    /// <summary>
    /// Boolean representing whether or not members from within the same group on a group assignment can be assigned to peer review their own group&apos;s work
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? IntraGroupPeerReviews { get; init; }
    /// <summary>
    /// the lock date (assignment is locked after this date). returns null if not present. NOTE: If this assignment has assignment overrides, this field will be the lock date as it applies to the user requesting information from the API.
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? LockAt { get; init; }
    /// <summary>
    /// (Optional) An explanation of why this is locked for the user. Present when locked_for_user is true.
    /// </summary> 
    /// <example>
    /// &quot;This assignment is locked until September 1 at 12:00am&quot;
    /// </example>
    public string? LockExplanation { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public LockInfo? LockInfo { get; init; }
    /// <summary>
    /// Whether or not this is locked for the user.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? LockedForUser { get; init; }
    /// <summary>
    /// An integer indicating the maximum length an assignment&apos;s name may be
    /// </summary> 
    /// <example>
    /// 15
    /// </example>
    public int? MaxNameLength { get; init; }
    /// <summary>
    /// Boolean indicating if the assignment is moderated.
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? ModeratedGrading { get; init; }
    /// <summary>
    /// whether the assignment is muted
    /// </summary>
    public bool? Muted { get; init; }
    /// <summary>
    /// the name of the assignment
    /// </summary> 
    /// <example>
    /// &quot;some assignment&quot;
    /// </example>
    public string? Name { get; init; }
    /// <summary>
    /// if the requesting user has grading rights, the number of submissions that need grading.
    /// </summary> 
    /// <example>
    /// 17
    /// </example>
    public int? NeedsGradingCount { get; init; }
    /// <summary>
    /// if the requesting user has grading rights and the &apos;needs_grading_count_by_section&apos; flag is specified, the number of submissions that need grading split out by section. NOTE: This key is NOT present unless you pass the &apos;needs_grading_count_by_section&apos; argument as true.  ANOTHER NOTE: it&apos;s possible to be enrolled in multiple sections, and if a student is setup that way they will show an assignment that needs grading in multiple sections (effectively the count will be duplicated between sections)
    /// </summary> 
    /// <example>
    /// []
    /// </example>
    public IList<NeedsGradingCount>? NeedsGradingCountBySection { get; init; }
    /// <summary>
    /// (Optional) If true, the assignment will be omitted from the student&apos;s final grade
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? OmitFromFinalGrade { get; init; }
    /// <summary>
    /// Whether the assignment is only visible to overrides.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? OnlyVisibleToOverrides { get; init; }
    /// <summary>
    /// (Optional) If &apos;overrides&apos; is included in the &apos;include&apos; parameter, includes an array of assignment override objects.
    /// </summary>
    public IList<AssignmentOverride>? Overrides { get; init; }
    /// <summary>
    /// Integer representing the amount of reviews each user is assigned. NOTE: This key is NOT present unless you have automatic_peer_reviews set to true.
    /// </summary> 
    /// <example>
    /// 0
    /// </example>
    public int? PeerReviewCount { get; init; }
    /// <summary>
    /// Boolean indicating if peer reviews are required for this assignment
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? PeerReviews { get; init; }
    /// <summary>
    /// String representing a date the reviews are due by. Must be a date that occurs after the default due date. If blank, or date is not after the assignment&apos;s due date, the assignment&apos;s due date will be used. NOTE: This key is NOT present unless you have automatic_peer_reviews set to true.
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? PeerReviewsAssignAt { get; init; }
    /// <summary>
    /// the maximum points possible for the assignment
    /// </summary> 
    /// <example>
    /// 12
    /// </example>
    public  double ? PointsPossible { get; init; }
    /// <summary>
    /// the sorting order of the assignment in the group
    /// </summary> 
    /// <example>
    /// 1
    /// </example>
    public int? Position { get; init; }
    /// <summary>
    /// (optional, present if Sync Grades to SIS feature is enabled)
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? PostToSis { get; init; }
    /// <summary>
    /// Whether the assignment is published
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? Published { get; init; }
    /// <summary>
    /// (Optional) id of the associated quiz (applies only when submission_types is [&apos;online_quiz&apos;])
    /// </summary> 
    /// <example>
    /// 620
    /// </example>
    public int? QuizId { get; init; }
    /// <summary>
    /// (Optional) A list of scoring criteria and ratings for each rubric criterion. Included if there is an associated rubric.
    /// </summary>
    public IList<RubricCriteria>? Rubric { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public RubricSettings? RubricSettings { get; init; }
    /// <summary>
    /// the types of submissions allowed for this assignment list containing one or more of the following: &apos;discussion_topic&apos;, &apos;online_quiz&apos;, &apos;on_paper&apos;, &apos;none&apos;, &apos;external_tool&apos;, &apos;online_text_entry&apos;, &apos;online_url&apos;, &apos;online_upload&apos; &apos;media_recording&apos;
    /// </summary> 
    /// <example>
    /// [&quot;online_text_entry&quot;]
    /// </example>
    public IList<string>? SubmissionTypes { get; init; }
    /// <summary>
    /// the URL to download all submissions as a zip
    /// </summary> 
    /// <example>
    /// &quot;https://example.com/courses/:course_id/assignments/:id/submissions?zip=1&quot;
    /// </example>
    public string? SubmissionsDownloadUrl { get; init; }
    /// <summary>
    /// Boolean flag indicating whether or not Turnitin has been enabled for the assignment. NOTE: This flag will not appear unless your account has the Turnitin plugin available
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? TurnitinEnabled { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public TurnitinSettings? TurnitinSettings { get; init; }
    /// <summary>
    /// the unlock date (assignment is unlocked after this date) returns null if not present NOTE: If this assignment has assignment overrides, this field will be the unlock date as it applies to the user requesting information from the API.
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? UnlockAt { get; init; }
    /// <summary>
    /// Whether the assignment&apos;s &apos;published&apos; state can be changed to false. Will be false if there are student submissions for the assignment.
    /// </summary> 
    /// <example>
    /// False
    /// </example>
    public bool? Unpublishable { get; init; }
    /// <summary>
    /// The time at which this assignment was last modified in any way
    /// </summary> 
    /// <example>
    /// 2012-07-01T23:59:00.0000000-06:00
    /// </example>
    public DateTime? UpdatedAt { get; init; }
    /// <summary>
    /// (Optional) If true, the rubric is directly tied to grading the assignment. Otherwise, it is only advisory. Included if there is an associated rubric.
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? UseRubricForGrading { get; init; }
    /// <summary>
    /// Boolean flag indicating whether or not VeriCite has been enabled for the assignment. NOTE: This flag will not appear unless your account has the VeriCite plugin available
    /// </summary> 
    /// <example>
    /// True
    /// </example>
    public bool? VericiteEnabled { get; init; }

    internal static Assignment? From(ICanvasClient client, AssignmentDto? dto)
    {
        if (dto == null)
            return null;
        return new Assignment(client)
        {
            AllDates = AssignmentDate.From(client, dto.AllDates),
            AllowedExtensions = dto.AllowedExtensions,
            AnonymousSubmissions = dto.AnonymousSubmissions,
            AssignmentGroupId = AssignmentGroupIdentifier.FromNullable(dto.AssignmentGroupId),
            AssignmentVisibility = dto.AssignmentVisibility,
            AutomaticPeerReviews = dto.AutomaticPeerReviews,
            CourseId = dto.CourseId == null ? CourseIdentifier.None : CourseIdentifier.From(dto.CourseId),
            CreatedAt = dto.CreatedAt,
            Description = dto.Description,
            DueAt = dto.DueAt,
            DueDateRequired = dto.DueDateRequired,
            ExternalToolTagAttributes = ExternalToolTagAttributes.From(client, dto.ExternalToolTagAttributes),
            FreezeOnCopy = dto.FreezeOnCopy,
            Frozen = dto.Frozen,
            FrozenAttributes = dto.FrozenAttributes,
            GradeGroupStudentsIndividually = dto.GradeGroupStudentsIndividually,
            GradingStandardId = dto.GradingStandardId,
            GradingType = dto.GradingType,
            GroupCategoryId = dto.GroupCategoryId,
            HasOverrides = dto.HasOverrides,
            HasSubmittedSubmissions = dto.HasSubmittedSubmissions,
            HtmlUrl = dto.HtmlUrl,
            Id = dto.Id == null ? AssignmentIdentifier.None : AssignmentIdentifier.From(dto.Id),
            IntegrationData = dto.IntegrationData,
            IntegrationId = dto.IntegrationId,
            IntraGroupPeerReviews = dto.IntraGroupPeerReviews,
            LockAt = dto.LockAt,
            LockExplanation = dto.LockExplanation,
            LockInfo = LockInfo.From(client, dto.LockInfo),
            LockedForUser = dto.LockedForUser,
            MaxNameLength = dto.MaxNameLength,
            ModeratedGrading = dto.ModeratedGrading,
            Muted = dto.Muted,
            Name = dto.Name,
            NeedsGradingCount = dto.NeedsGradingCount,
            NeedsGradingCountBySection = Entities.NeedsGradingCount.From(client, dto.NeedsGradingCountBySection),
            OmitFromFinalGrade = dto.OmitFromFinalGrade,
            OnlyVisibleToOverrides = dto.OnlyVisibleToOverrides,
            Overrides = AssignmentOverride.From(client, dto.Overrides),
            PeerReviewCount = dto.PeerReviewCount,
            PeerReviews = dto.PeerReviews,
            PeerReviewsAssignAt = dto.PeerReviewsAssignAt,
            PointsPossible = dto.PointsPossible,
            Position = dto.Position,
            PostToSis = dto.PostToSis,
            Published = dto.Published,
            QuizId = dto.QuizId,
            Rubric = RubricCriteria.From(client, dto.Rubric),
            RubricSettings = RubricSettings.From(client, dto.RubricSettings),
            SubmissionTypes = dto.SubmissionTypes,
            SubmissionsDownloadUrl = dto.SubmissionsDownloadUrl,
            TurnitinEnabled = dto.TurnitinEnabled,
            TurnitinSettings = TurnitinSettings.From(client, dto.TurnitinSettings),
            UnlockAt = dto.UnlockAt,
            Unpublishable = dto.Unpublishable,
            UpdatedAt = dto.UpdatedAt,
            UseRubricForGrading = dto.UseRubricForGrading,
            VericiteEnabled = dto.VericiteEnabled,
            _source = dto,
        };
    }

    internal static IList<Assignment> From(ICanvasClient client, IEnumerable<AssignmentDto>? dto)
    {
        if (dto == null)
            return[];
        return[..dto.Select(item => From(client, item)!)];
    }
}