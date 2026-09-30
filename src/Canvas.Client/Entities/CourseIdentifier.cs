using Vogen;

namespace Canvas.Client.Entities;

/// <summary>
/// An identifier for a course.
/// </summary>
[ValueObject<string>(Conversions.SystemTextJson)]
public readonly partial struct CourseIdentifier
{
    /// <summary>
    /// The identifier has not been set.
    /// </summary>
    public static readonly CourseIdentifier None = new(string.Empty);

    /// <summary>
    /// Generates an <see cref="CourseIdentifier"/> from a SIS ID.
    /// </summary>
    /// <param name="sisId">The SIS ID.</param>
    /// <returns>An <see cref="CourseIdentifier"/> in the SIS ID format.</returns>
    public static CourseIdentifier FromSisId(string sisId) => From($"sis_course_id:{sisId}");

    /// <summary>
    /// Determines whether one <see cref="CourseIdentifier"/> has an earlier value than another 
    /// <see cref="CourseIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <(CourseIdentifier first, CourseIdentifier second) => first.CompareTo(second) < 0;

    /// <summary>
    /// Determines whether one <see cref="CourseIdentifier"/> has an earlier or same value as another 
    /// <see cref="CourseIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(CourseIdentifier first, CourseIdentifier second) => first.CompareTo(second) <= 0;

    /// <summary>
    /// Determines whether one <see cref="CourseIdentifier"/> has a later value than another 
    /// <see cref="CourseIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >(CourseIdentifier first, CourseIdentifier second) => first.CompareTo(second) > 0;

    /// <summary>
    /// Determines whether one <see cref="CourseIdentifier"/> has a later or same value as another 
    /// <see cref="CourseIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(CourseIdentifier first, CourseIdentifier second) => first.CompareTo(second) >= 0;

    private static Validation Validate(string input) => !string.IsNullOrEmpty(input)
        ? Validation.Ok
        : Validation.Invalid("Course identifiers must not be empty.");
}
