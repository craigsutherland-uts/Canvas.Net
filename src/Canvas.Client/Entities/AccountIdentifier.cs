using Vogen;

namespace Canvas.Client.Entities;

/// <summary>
/// An identifier for an account.
/// </summary>
[ValueObject<string>(Conversions.SystemTextJson)]
public readonly partial struct AccountIdentifier
{
    /// <summary>
    /// The account identifier for the default account.
    /// </summary>
    public static readonly AccountIdentifier Default = From("default");

    /// <summary>
    /// The identifier has not been set.
    /// </summary>
    public static readonly AccountIdentifier None = new(string.Empty);

    /// <summary>
    /// The account identifier for the self account.
    /// </summary>
    public static readonly AccountIdentifier Self = From("self");

    /// <summary>
    /// The account identifier for the site admin account.
    /// </summary>
    public static readonly AccountIdentifier SiteAdmin = From("site_admin");

    /// <summary>
    /// Generates an <see cref="AccountIdentifier"/> from a SIS ID.
    /// </summary>
    /// <param name="sisId">The SIS ID.</param>
    /// <returns>An <see cref="AccountIdentifier"/> in the SIS ID format.</returns>
    public static AccountIdentifier FromSisId(string sisId) => From($"sis_account_id:{sisId}");

    /// <summary>
    /// Determines whether one <see cref="AccountIdentifier"/> has an earlier value than another 
    /// <see cref="AccountIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <(AccountIdentifier first, AccountIdentifier second) => first.CompareTo(second) < 0;

    /// <summary>
    /// Determines whether one <see cref="AccountIdentifier"/> has an earlier or same value as another 
    /// <see cref="AccountIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(AccountIdentifier first, AccountIdentifier second) => first.CompareTo(second) <= 0;

    /// <summary>
    /// Determines whether one <see cref="AccountIdentifier"/> has a later value than another 
    /// <see cref="AccountIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >(AccountIdentifier first, AccountIdentifier second) => first.CompareTo(second) > 0;

    /// <summary>
    /// Determines whether one <see cref="AccountIdentifier"/> has a later or same value as another 
    /// <see cref="AccountIdentifier"/>.
    /// </summary>
    /// <param name="first">The first id to compare.</param>
    /// <param name="second">The second id to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="first"/> is earlier than <paramref name="second"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(AccountIdentifier first, AccountIdentifier second) => first.CompareTo(second) >= 0;

    private static Validation Validate(string input) => !string.IsNullOrEmpty(input)
        ? Validation.Ok
        : Validation.Invalid("Account identifiers must not be empty.");
}
