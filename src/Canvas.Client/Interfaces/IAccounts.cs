using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level account interactions.
/// </summary>
public interface IAccounts
{
    /// <summary>
    /// Lists the accounts for the user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Account}"/> containing the accounts.</returns>
    IAsyncEnumerable<Account?> List(CancellationToken cancellationToken);

    /// <summary>
    /// Lists the accounts for the user.
    /// </summary>
    /// <param name="options">The options for the request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Account}"/> containing the accounts.</returns>
    IAsyncEnumerable<Account?> List(CanvasOptions options, CancellationToken cancellationToken);

    /// <summary>
    /// Starts a new <see cref="Account"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Account"/> instance.</returns>
    Account New();

    /// <summary>
    /// Retrieves an account.
    /// </summary>
    /// <param name="identifier">The identifier of the account.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Account"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<Account?> Retrieve(AccountIdentifier identifier, CancellationToken cancellationToken);
}
