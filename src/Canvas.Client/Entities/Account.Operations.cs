using System.Diagnostics;

namespace Canvas.Client.Entities;

[DebuggerDisplay($"{{{nameof(Name)}}} [{{{nameof(Id)}.ToString()}}]")]
public partial record Account
{
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
        var refreshed = await _client
                .Accounts
                .Retrieve(Id, cancellationToken)
                .ConfigureAwait(false)
            ?? throw new CanvasClientException("Unable to refresh account: Canvas returned a not found result");
        return refreshed;
    }
}
