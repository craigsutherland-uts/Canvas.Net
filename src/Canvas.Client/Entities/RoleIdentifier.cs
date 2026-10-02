using Vogen;

namespace Canvas.Client.Entities;

/// <summary>
/// An identifier for a role.
/// </summary>
[ValueObject<string>(Conversions.SystemTextJson)]
public readonly partial struct RoleIdentifier
{
    /// <summary>
    /// The identifier has not been set.
    /// </summary>
    public static readonly RoleIdentifier None = new(string.Empty);

    /// <summary>
    /// Determines whether one <see cref="RoleIdentifier"/> has an earlier value than another 
    /// <see cref="RoleIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <(RoleIdentifier first, RoleIdentifier second) => first.CompareTo(second) < 0;

    /// <summary>
    /// Determines whether one <see cref="RoleIdentifier"/> has an earlier or same value as another 
    /// <see cref="RoleIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(RoleIdentifier first, RoleIdentifier second) => first.CompareTo(second) <= 0;

    /// <summary>
    /// Determines whether one <see cref="RoleIdentifier"/> has a later value than another 
    /// <see cref="RoleIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >(RoleIdentifier first, RoleIdentifier second) => first.CompareTo(second) > 0;

    /// <summary>
    /// Determines whether one <see cref="RoleIdentifier"/> has a later or same value as another 
    /// <see cref="RoleIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(RoleIdentifier first, RoleIdentifier second) => first.CompareTo(second) >= 0;

    private static Validation Validate(string input) => !string.IsNullOrEmpty(input)
        ? Validation.Ok
        : Validation.Invalid("Role identifiers must not be empty.");
}
