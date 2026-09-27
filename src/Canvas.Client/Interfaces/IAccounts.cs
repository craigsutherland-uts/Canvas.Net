using Canvas.Client.Entities;

namespace Canvas.Client.Interfaces;

/// <summary>
/// The root-level account interactions.
/// </summary>
public interface IAccounts
{
    /// <summary>
    /// Retrieves an account.
    /// </summary>
    /// <param name="identifier">The identifier of the account.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Account"/> instance if found; <see langword="null"/> otherwise.</returns>
    Task<Account?> Retrieve(AccountIdentifier identifier, CancellationToken cancellationToken);
}
