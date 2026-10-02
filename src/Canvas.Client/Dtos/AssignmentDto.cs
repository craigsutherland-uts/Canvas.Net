using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Assignment entities.
/// </summary>
internal sealed partial record AssignmentDto
{
    [JsonPropertyName("all_dates")]
    public IList<AssignmentDateDto>? AllDates { get; init; }

    [JsonPropertyName("allowed_extensions")]
    public IList<string>? AllowedExtensions { get; init; }

    [JsonPropertyName("anonymous_submissions")]
    public bool? AnonymousSubmissions { get; init; }

    [JsonPropertyName("assignment_group_id")]
    public int? AssignmentGroupId { get; init; }

    [JsonPropertyName("assignment_visibility")]
    public IList<int>? AssignmentVisibility { get; init; }

    [JsonPropertyName("automatic_peer_reviews")]
    public bool? AutomaticPeerReviews { get; init; }

    [JsonPropertyName("course_id")]
    public int? CourseId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; init; }
    public string? Description { get; init; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; init; }

    [JsonPropertyName("due_date_required")]
    public bool? DueDateRequired { get; init; }

    [JsonPropertyName("external_tool_tag_attributes")]
    public ExternalToolTagAttributesDto? ExternalToolTagAttributes { get; init; }

    [JsonPropertyName("freeze_on_copy")]
    public bool? FreezeOnCopy { get; init; }
    public bool? Frozen { get; init; }

    [JsonPropertyName("frozen_attributes")]
    public IList<string>? FrozenAttributes { get; init; }

    [JsonPropertyName("grade_group_students_individually")]
    public bool? GradeGroupStudentsIndividually { get; init; }

    [JsonPropertyName("grading_standard_id")]
    public int? GradingStandardId { get; init; }

    [JsonPropertyName("grading_type")]
    public string? GradingType { get; init; }

    [JsonPropertyName("group_category_id")]
    public int? GroupCategoryId { get; init; }

    [JsonPropertyName("has_overrides")]
    public bool? HasOverrides { get; init; }

    [JsonPropertyName("has_submitted_submissions")]
    public bool? HasSubmittedSubmissions { get; init; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; init; }
    public int? Id { get; init; }

    [JsonPropertyName("integration_data")]
    public object? IntegrationData { get; init; }

    [JsonPropertyName("integration_id")]
    public string? IntegrationId { get; init; }

    [JsonPropertyName("intra_group_peer_reviews")]
    public bool? IntraGroupPeerReviews { get; init; }

    [JsonPropertyName("lock_at")]
    public DateTime? LockAt { get; init; }

    [JsonPropertyName("lock_explanation")]
    public string? LockExplanation { get; init; }

    [JsonPropertyName("lock_info")]
    public LockInfoDto? LockInfo { get; init; }

    [JsonPropertyName("locked_for_user")]
    public bool? LockedForUser { get; init; }

    [JsonPropertyName("max_name_length")]
    public int? MaxNameLength { get; init; }

    [JsonPropertyName("moderated_grading")]
    public bool? ModeratedGrading { get; init; }
    public bool? Muted { get; init; }
    public string? Name { get; init; }

    [JsonPropertyName("needs_grading_count")]
    public int? NeedsGradingCount { get; init; }

    [JsonPropertyName("needs_grading_count_by_section")]
    public IList<NeedsGradingCountDto>? NeedsGradingCountBySection { get; init; }

    [JsonPropertyName("omit_from_final_grade")]
    public bool? OmitFromFinalGrade { get; init; }

    [JsonPropertyName("only_visible_to_overrides")]
    public bool? OnlyVisibleToOverrides { get; init; }
    public IList<AssignmentOverrideDto>? Overrides { get; init; }

    [JsonPropertyName("peer_review_count")]
    public int? PeerReviewCount { get; init; }

    [JsonPropertyName("peer_reviews")]
    public bool? PeerReviews { get; init; }

    [JsonPropertyName("peer_reviews_assign_at")]
    public DateTime? PeerReviewsAssignAt { get; init; }

    [JsonPropertyName("points_possible")]
    public int? PointsPossible { get; init; }
    public int? Position { get; init; }

    [JsonPropertyName("post_to_sis")]
    public bool? PostToSis { get; init; }
    public bool? Published { get; init; }

    [JsonPropertyName("quiz_id")]
    public int? QuizId { get; init; }
    public IList<RubricCriteriaDto>? Rubric { get; init; }

    [JsonPropertyName("rubric_settings")]
    public RubricSettingsDto? RubricSettings { get; init; }

    [JsonPropertyName("submission_types")]
    public IList<string>? SubmissionTypes { get; init; }

    [JsonPropertyName("submissions_download_url")]
    public string? SubmissionsDownloadUrl { get; init; }

    [JsonPropertyName("turnitin_enabled")]
    public bool? TurnitinEnabled { get; init; }

    [JsonPropertyName("turnitin_settings")]
    public TurnitinSettingsDto? TurnitinSettings { get; init; }

    [JsonPropertyName("unlock_at")]
    public DateTime? UnlockAt { get; init; }
    public bool? Unpublishable { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; init; }

    [JsonPropertyName("use_rubric_for_grading")]
    public bool? UseRubricForGrading { get; init; }

    [JsonPropertyName("vericite_enabled")]
    public bool? VericiteEnabled { get; init; }
}