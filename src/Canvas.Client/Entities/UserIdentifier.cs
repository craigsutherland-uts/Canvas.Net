using System.Globalization;

using Vogen;

namespace Canvas.Client.Entities;

/// <summary>
/// An identifier for a user.
/// </summary>
[ValueObject<string>(Conversions.SystemTextJson)]
public readonly partial struct UserIdentifier
{
    /// <summary>
    /// The identifier has not been set.
    /// </summary>
    public static readonly UserIdentifier None = new(string.Empty);

    /// <summary>
    /// The self identifier.
    /// </summary>
    public static readonly UserIdentifier Self = From("self");

    /// <summary>
    /// Generate a new <see cref="UserIdentifier"/> from a user ID.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The new <see cref="UserIdentifier"/> instance.</returns>
    public static UserIdentifier From(int userId) => From(userId.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Determines whether one <see cref="UserIdentifier"/> has an earlier value than another 
    /// <see cref="UserIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <(UserIdentifier first, UserIdentifier second) => first.CompareTo(second) < 0;

    /// <summary>
    /// Determines whether one <see cref="UserIdentifier"/> has an earlier or same value as another 
    /// <see cref="UserIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(UserIdentifier first, UserIdentifier second) => first.CompareTo(second) <= 0;

    /// <summary>
    /// Determines whether one <see cref="UserIdentifier"/> has a later value than another 
    /// <see cref="UserIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >(UserIdentifier first, UserIdentifier second) => first.CompareTo(second) > 0;

    /// <summary>
    /// Determines whether one <see cref="UserIdentifier"/> has a later or same value as another 
    /// <see cref="UserIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(UserIdentifier first, UserIdentifier second) => first.CompareTo(second) >= 0;

    private static Validation Validate(string input) => !string.IsNullOrEmpty(input)
        ? Validation.Ok
        : Validation.Invalid("User identifiers must not be empty.");
}
