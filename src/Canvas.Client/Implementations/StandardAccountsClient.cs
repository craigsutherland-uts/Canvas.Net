using Canvas.Client.Dtos;
using Canvas.Client.Entities;
using Canvas.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="IAccounts"/>.
/// </summary>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
/// <param name="options">The <see cref="ICanvasOptions"/> to use when connecting to Canvas.</param>
[CanvasClient<IAccounts>]
public sealed class StandardAccountsClient(
        ICanvasConnection connection,
        ILogger<StandardAccountsClient> logger,
        ICanvasOptions options)
    : IAccounts
{
    /// <summary>
    /// Starts a new <see cref="Account"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Account"/> instance.</returns>
    public Account New()
    {
        return new Account(this)
        {
            Id = AccountIdentifier.None,
        };
    }

    /// <summary>
    /// Retrieves an account.
    /// </summary>
    /// <param name="identifier">The identifier of the account.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Account"/> instance if found; <see langword="null"/> otherwise.</returns>
    public async Task<Account?> Retrieve(AccountIdentifier identifier, CancellationToken cancellationToken)
    {
        logger.LogDebug("Retrieving account with id {id}", identifier);
        var dto = await connection.GetEntity<AccountDto>(
                string.Create(CultureInfo.InvariantCulture, $"/api/v1/accounts/{identifier}"),
                options,
                cancellationToken)
            .ConfigureAwait(false);
        return Account.From(this, dto);
    }
}