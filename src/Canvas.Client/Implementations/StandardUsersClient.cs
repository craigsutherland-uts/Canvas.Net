using Canvas.Client.Entities;

using Microsoft.Extensions.Logging;

using System.Globalization;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="IUsers"/>.
/// </summary>
/// <param name="parent">The parent <see cref="ICanvasClient"/>.</param>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
/// <param name="options">The <see cref="ICanvasOptions"/> to use when connecting to Canvas.</param>
[CanvasClient<IUsers>]
public sealed class StandardUsersClient(
        ICanvasClient parent,
        ICanvasConnection connection,
        ILogger<StandardUsersClient> logger,
        ICanvasOptions options)
    : IUsers
{
    /// <summary>
    /// Starts a new <see cref="User"/> instance.
    /// </summary>
    /// <returns>A new <see cref="User"/> instance.</returns>
    public User New()
    {
        return new User(parent)
        {
            Id = UserIdentifier.None,
        };
    }

    /// <summary>
    /// Retrieves a user.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="User"/> instance if found; <see langword="null"/> otherwise.</returns>
    public async Task<User?> Retrieve(UserIdentifier identifier, CancellationToken cancellationToken)
    {
        logger.LogDebug("Retrieving user with id {id}", identifier);
        var dto = await connection.GetEntity<UserDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/users/{identifier}"),
                options,
                cancellationToken)
            .ConfigureAwait(false);
        return User.From(parent, dto);
    }

    /// <summary>
    /// Retrieves the current user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="User"/> instance if found; <see langword="null"/> otherwise.</returns>
    public Task<User?> RetrieveSelf(CancellationToken cancellationToken)
    {
        return Retrieve(UserIdentifier.Self, cancellationToken);
    }
}