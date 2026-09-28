using Canvas.Client.Interfaces;
using CommunityToolkit.Diagnostics;

namespace Canvas.Client.Entities;

public partial record Account
{
    private readonly IAccounts _accountsClient;

    /// <summary>
    /// Initialise a new <see cref="Account"/> instance.
    /// </summary>
    /// <param name="accountsClient">The underlying <see cref="IAccounts"/>.</param>
    internal Account(IAccounts accountsClient)
    {
        Guard.IsNotNull(accountsClient);
        _accountsClient = accountsClient;
    }

    /// <summary>
    /// Refreshes the details from Canvas.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A new <see cref="Account"/> instance with the refreshed details.</returns>
    /// <remarks>
    /// This method will return a new instance of the <see cref="Account"/>, it does not modify
    /// the existing instance.
    /// </remarks>
    public async Task<Account> Refresh(CancellationToken cancellationToken)
    {
        var refreshed = await _accountsClient
                .Retrieve(Id, cancellationToken)
                .ConfigureAwait(false)
            ?? throw new CanvasClientException("Unable to refresh account: Canvas returned a not found result");
        return refreshed;
    }
}
