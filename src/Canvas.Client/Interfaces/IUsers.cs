using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level user interactions.
/// </summary>
public interface IUsers
{
    /// <summary>
    /// Starts a new <see cref="User"/> instance.
    /// </summary>
    /// <returns>A new <see cref="User"/> instance.</returns>
    User New();

    /// <summary>
    /// Retrieves a user.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="User"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<User?> Retrieve(UserIdentifier identifier, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the current user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="User"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<User?> RetrieveSelf(CancellationToken cancellationToken);
}
