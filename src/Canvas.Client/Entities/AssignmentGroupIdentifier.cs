using Vogen;

namespace Canvas.Client.Entities;

/// <summary>
/// An identifier for an assignment group.
/// </summary>
[ValueObject<string>(Conversions.SystemTextJson)]
public readonly partial struct AssignmentGroupIdentifier
{
    /// <summary>
    /// The identifier has not been set.
    /// </summary>
    public static readonly AssignmentGroupIdentifier None = new(string.Empty);

    /// <summary>
    /// Determines whether one <see cref="AssignmentGroupIdentifier"/> has an earlier value than another 
    /// <see cref="AssignmentGroupIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <(AssignmentGroupIdentifier first, AssignmentGroupIdentifier second) => first.CompareTo(second) < 0;

    /// <summary>
    /// Determines whether one <see cref="AssignmentGroupIdentifier"/> has an earlier or same value as another 
    /// <see cref="AssignmentGroupIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(AssignmentGroupIdentifier first, AssignmentGroupIdentifier second) => first.CompareTo(second) <= 0;

    /// <summary>
    /// Determines whether one <see cref="AssignmentGroupIdentifier"/> has a later value than another 
    /// <see cref="AssignmentGroupIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >(AssignmentGroupIdentifier first, AssignmentGroupIdentifier second) => first.CompareTo(second) > 0;

    /// <summary>
    /// Determines whether one <see cref="AssignmentGroupIdentifier"/> has a later or same value as another 
    /// <see cref="AssignmentGroupIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(AssignmentGroupIdentifier first, AssignmentGroupIdentifier second) => first.CompareTo(second) >= 0;

    private static Validation Validate(string input) => !string.IsNullOrEmpty(input)
        ? Validation.Ok
        : Validation.Invalid("Assignment group identifiers must not be empty.");
}
