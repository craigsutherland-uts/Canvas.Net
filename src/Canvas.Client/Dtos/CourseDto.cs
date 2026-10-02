using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Course entities.
/// </summary>
internal sealed partial record CourseDto
{
    [JsonPropertyName("access_restricted_by_date")]
    public bool? AccessRestrictedByDate { get; init; }

    [JsonPropertyName("account_id")]
    public  string ? AccountId { get; init; }

    [JsonPropertyName("allow_student_assignment_edits")]
    public bool? AllowStudentAssignmentEdits { get; init; }

    [JsonPropertyName("allow_student_forum_attachments")]
    public bool? AllowStudentForumAttachments { get; init; }

    [JsonPropertyName("allow_wiki_comments")]
    public bool? AllowWikiComments { get; init; }

    [JsonPropertyName("apply_assignment_group_weights")]
    public bool? ApplyAssignmentGroupWeights { get; init; }
    public bool? Blueprint { get; init; }

    [JsonPropertyName("blueprint_restrictions")]
    public object? BlueprintRestrictions { get; init; }

    [JsonPropertyName("blueprint_restrictions_by_object_type")]
    public object? BlueprintRestrictionsByObjectType { get; init; }
    public CalendarLinkDto? Calendar { get; init; }

    [JsonPropertyName("course_code")]
    public string? CourseCode { get; init; }

    [JsonPropertyName("course_color")]
    public string? CourseColor { get; init; }

    [JsonPropertyName("course_format")]
    public string? CourseFormat { get; init; }

    [JsonPropertyName("course_progress")]
    public CourseProgressDto? CourseProgress { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; init; }

    [JsonPropertyName("default_view")]
    public string? DefaultView { get; init; }

    [JsonPropertyName("end_at")]
    public DateTime? EndAt { get; init; }

    [JsonPropertyName("enrollment_term_id")]
    public  string ? EnrollmentTermId { get; init; }
    public IList<EnrollmentDto>? Enrollments { get; init; }

    [JsonPropertyName("friendly_name")]
    public string? FriendlyName { get; init; }

    [JsonPropertyName("grade_passback_setting")]
    public string? GradePassbackSetting { get; init; }

    [JsonPropertyName("grading_periods")]
    public IList<GradingPeriodDto>? GradingPeriods { get; init; }

    [JsonPropertyName("grading_standard_id")]
    public int? GradingStandardId { get; init; }

    [JsonPropertyName("hide_final_grades")]
    public bool? HideFinalGrades { get; init; }

    [JsonPropertyName("homeroom_course")]
    public bool? HomeroomCourse { get; init; }
    public  string ? Id { get; init; }

    [JsonPropertyName("integration_id")]
    public string? IntegrationId { get; init; }

    [JsonPropertyName("is_public")]
    public bool? IsPublic { get; init; }

    [JsonPropertyName("is_public_to_auth_users")]
    public bool? IsPublicToAuthUsers { get; init; }
    public string? License { get; init; }
    public string? Locale { get; init; }
    public string? Name { get; init; }

    [JsonPropertyName("needs_grading_count")]
    public int? NeedsGradingCount { get; init; }

    [JsonPropertyName("open_enrollment")]
    public bool? OpenEnrollment { get; init; }

    [JsonPropertyName("original_name")]
    public string? OriginalName { get; init; }
    public object? Permissions { get; init; }

    [JsonPropertyName("public_description")]
    public string? PublicDescription { get; init; }

    [JsonPropertyName("public_syllabus")]
    public bool? PublicSyllabus { get; init; }

    [JsonPropertyName("public_syllabus_to_auth")]
    public bool? PublicSyllabusToAuth { get; init; }

    [JsonPropertyName("restrict_enrollments_to_course_dates")]
    public bool? RestrictEnrollmentsToCourseDates { get; init; }

    [JsonPropertyName("root_account_id")]
    public  string ? RootAccountId { get; init; }

    [JsonPropertyName("self_enrollment")]
    public bool? SelfEnrollment { get; init; }

    [JsonPropertyName("sis_course_id")]
    public string? SisCourseId { get; init; }

    [JsonPropertyName("sis_import_id")]
    public  string ? SisImportId { get; init; }

    [JsonPropertyName("start_at")]
    public DateTime? StartAt { get; init; }

    [JsonPropertyName("storage_quota_mb")]
    public int? StorageQuotaMb { get; init; }

    [JsonPropertyName("storage_quota_used_mb")]
    public double? StorageQuotaUsedMb { get; init; }

    [JsonPropertyName("syllabus_body")]
    public string? SyllabusBody { get; init; }
    public bool? Template { get; init; }
    public TermDto? Term { get; init; }

    [JsonPropertyName("time_zone")]
    public string? TimeZone { get; init; }

    [JsonPropertyName("total_students")]
    public int? TotalStudents { get; init; }
    public string? Uuid { get; init; }

    [JsonPropertyName("workflow_state")]
    public string? WorkflowState { get; init; }
}