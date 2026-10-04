using Canvas.Client.Entities;

using Microsoft.Extensions.Logging;

using System.Globalization;
using System.Runtime.CompilerServices;

namespace Canvas.Client.Implementations;

/// <summary>
/// The default implementation of <see cref="IAccounts"/>.
/// </summary>
/// <param name="parent">The parent <see cref="ICanvasClient"/>.</param>
/// <param name="connection">The <see cref="ICanvasConnection"/> instance to use.</param>
/// <param name="logger">A <see cref="ILogger"/> for logging any messages.</param>
[CanvasClient<IAccounts>]
public sealed class StandardAccountsClient(
        ICanvasClient parent,
        ICanvasConnection connection,
        ILogger<StandardAccountsClient> logger)
    : IAccounts
{
    /// <summary>
    /// Lists the accounts for the user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Account}"/> containing the accounts.</returns>
    public IAsyncEnumerable<Account?> List(
        CancellationToken cancellationToken)
    {
        return List(CanvasOptions.New(), cancellationToken);
    }

    /// <summary>
    /// Lists the accounts for the user.
    /// </summary>
    /// <param name="options">The options for the request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IAsyncEnumerable{Account}"/> containing the accounts.</returns>
    public async IAsyncEnumerable<Account?> List(
            CanvasOptions options,
            [EnumeratorCancellation] CancellationToken cancellationToken)
    {

        logger.LogDebug("Listing accounts for user");
        await foreach (var dto in connection.ListEntities<AccountDto>(
                "/api/v1/accounts",
                options,
                cancellationToken)
            .ConfigureAwait(false))
        {
            var entity = ParseDto(dto);
            if (entity != null) yield return entity;
        }
    }

    /// <summary>
    /// Starts a new <see cref="Account"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Account"/> instance.</returns>
    public Account New()
    {
        return new Account(parent)
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
                CanvasOptions.New(),
                cancellationToken)
            .ConfigureAwait(false);
        return ParseDto(dto);
    }

    /// <summary>
    /// Parses an <see cref="AccountDto"/> into an <see cref="Account"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="AccountDto"/> to parse.</param>
    /// <returns>The parsed <see cref="Account"/> entity, or <see langword="null"/> if the DTO is <see langword="null"/>.</returns>
    private Account? ParseDto(AccountDto? dto)
    {
        var entity = Account.From(parent, dto);
        return entity;
    }
}