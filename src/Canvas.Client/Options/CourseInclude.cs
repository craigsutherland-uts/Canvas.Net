namespace Canvas.Client.Options;

/// <summary>
/// Items that can be included when fetching courses.
/// </summary>
[Flags]
public enum CourseInclude
{
    /// <summary>
    /// Do not include anything.
    /// </summary>
    None = 0,

    /// <summary>
    /// Include the needs grading count.
    /// </summary>
    NeedsGradingCount = 1,

    /// <summary>
    /// Include the syllabus body.
    /// </summary>
    SyllabusBody = 2,

    /// <summary>
    /// Include the syllabus versions.
    /// </summary>
    SyllabusVersions = 4,

    /// <summary>
    /// Include the public description.
    /// </summary>
    PublicDescription = 8,

    /// <summary>
    /// Include the total scores.
    /// </summary>
    TotalScores = 16,

    /// <summary>
    /// Include the current grading period scores.
    /// </summary>
    CurrentGradingPeriodScores = 32,

    /// <summary>
    /// Include the grading periods.
    /// </summary>
    GradingPeriods = 64,

    /// <summary>
    /// Include the term.
    /// </summary>
    Term = 128,

    /// <summary>
    /// Include the account.
    /// </summary>
    Account = 256,

    /// <summary>
    /// Include the course progress.
    /// </summary>
    CourseProgress = 512,

    /// <summary>
    /// Include the sections.
    /// </summary>
    Sections = 1024,

    /// <summary>
    /// Include the storage quota used in MB.
    /// </summary>
    StorageQuotaUsedMb = 2048,

    /// <summary>
    /// Include the storage quota used percentage.
    /// </summary>
    TotalStudents = 4096,

    /// <summary>
    /// Include the passback status.
    /// </summary>
    PassbackStatus = 16384,

    /// <summary>
    /// Include the favorites.
    /// </summary>
    Favorites = 32768,

    /// <summary>
    /// Include the teachers.
    /// </summary>
    Teachers = 65536,

    /// <summary>
    /// Include the observed users.
    /// </summary>
    ObservedUsers = 131072,

    /// <summary>
    /// Include the course image.
    /// </summary>
    CourseImage = 262144,

    /// <summary>
    /// Include the banner image.
    /// </summary>
    BannerImage = 524288,

    /// <summary>
    /// Include the concluded status.
    /// </summary>
    Concluded = 1048576,

    /// <summary>
    /// Include the post manually status.
    /// </summary>
    PostManually = 2097152,

    /// <summary>
    /// Include all available items.
    /// </summary>
    All = ~None,
}