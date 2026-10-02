using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Enrollment entities.
/// </summary>
internal sealed partial record EnrollmentDto
{
    [JsonPropertyName("associated_user_id")]
    public int? AssociatedUserId { get; init; }

    [JsonPropertyName("computed_current_grade")]
    public string? ComputedCurrentGrade { get; init; }

    [JsonPropertyName("computed_current_score")]
    public double? ComputedCurrentScore { get; init; }

    [JsonPropertyName("computed_final_grade")]
    public string? ComputedFinalGrade { get; init; }

    [JsonPropertyName("computed_final_score")]
    public double? ComputedFinalScore { get; init; }

    [JsonPropertyName("course_id")]
    public int? CourseId { get; init; }

    [JsonPropertyName("course_integration_id")]
    public string? CourseIntegrationId { get; init; }

    [JsonPropertyName("course_section_id")]
    public int? CourseSectionId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; init; }

    [JsonPropertyName("current_grading_period_id")]
    public int? CurrentGradingPeriodId { get; init; }

    [JsonPropertyName("current_grading_period_title")]
    public string? CurrentGradingPeriodTitle { get; init; }

    [JsonPropertyName("current_period_computed_current_grade")]
    public string? CurrentPeriodComputedCurrentGrade { get; init; }

    [JsonPropertyName("current_period_computed_current_score")]
    public double? CurrentPeriodComputedCurrentScore { get; init; }

    [JsonPropertyName("current_period_computed_final_grade")]
    public string? CurrentPeriodComputedFinalGrade { get; init; }

    [JsonPropertyName("current_period_computed_final_score")]
    public double? CurrentPeriodComputedFinalScore { get; init; }

    [JsonPropertyName("current_period_override_grade")]
    public string? CurrentPeriodOverrideGrade { get; init; }

    [JsonPropertyName("current_period_override_score")]
    public double? CurrentPeriodOverrideScore { get; init; }

    [JsonPropertyName("current_period_unposted_current_grade")]
    public string? CurrentPeriodUnpostedCurrentGrade { get; init; }

    [JsonPropertyName("current_period_unposted_current_score")]
    public double? CurrentPeriodUnpostedCurrentScore { get; init; }

    [JsonPropertyName("current_period_unposted_final_grade")]
    public string? CurrentPeriodUnpostedFinalGrade { get; init; }

    [JsonPropertyName("current_period_unposted_final_score")]
    public double? CurrentPeriodUnpostedFinalScore { get; init; }

    [JsonPropertyName("end_at")]
    public DateTime? EndAt { get; init; }

    [JsonPropertyName("enrollment_state")]
    public string? EnrollmentState { get; init; }
    public GradeDto? Grades { get; init; }

    [JsonPropertyName("has_grading_periods")]
    public bool? HasGradingPeriods { get; init; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; init; }
    public int? Id { get; init; }

    [JsonPropertyName("last_activity_at")]
    public DateTime? LastActivityAt { get; init; }

    [JsonPropertyName("last_attended_at")]
    public DateTime? LastAttendedAt { get; init; }

    [JsonPropertyName("limit_privileges_to_course_section")]
    public bool? LimitPrivilegesToCourseSection { get; init; }

    [JsonPropertyName("override_grade")]
    public string? OverrideGrade { get; init; }

    [JsonPropertyName("override_score")]
    public double? OverrideScore { get; init; }
    public string? Role { get; init; }

    [JsonPropertyName("role_id")]
    public  string ? RoleId { get; init; }

    [JsonPropertyName("root_account_id")]
    public int? RootAccountId { get; init; }

    [JsonPropertyName("section_integration_id")]
    public string? SectionIntegrationId { get; init; }

    [JsonPropertyName("sis_account_id")]
    public string? SisAccountId { get; init; }

    [JsonPropertyName("sis_course_id")]
    public string? SisCourseId { get; init; }

    [JsonPropertyName("sis_import_id")]
    public int? SisImportId { get; init; }

    [JsonPropertyName("sis_section_id")]
    public string? SisSectionId { get; init; }

    [JsonPropertyName("sis_user_id")]
    public string? SisUserId { get; init; }

    [JsonPropertyName("start_at")]
    public DateTime? StartAt { get; init; }

    [JsonPropertyName("total_activity_time")]
    public int? TotalActivityTime { get; init; }

    [JsonPropertyName("totals_for_all_grading_periods_option")]
    public bool? TotalsForAllGradingPeriodsOption { get; init; }
    public string? Type { get; init; }

    [JsonPropertyName("unposted_current_grade")]
    public string? UnpostedCurrentGrade { get; init; }

    [JsonPropertyName("unposted_current_score")]
    public string? UnpostedCurrentScore { get; init; }

    [JsonPropertyName("unposted_final_grade")]
    public string? UnpostedFinalGrade { get; init; }

    [JsonPropertyName("unposted_final_score")]
    public double? UnpostedFinalScore { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; init; }
    public UserDto? User { get; init; }

    [JsonPropertyName("user_id")]
    public  string ? UserId { get; init; }
}