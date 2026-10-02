using System.Diagnostics;

namespace Canvas.Client.Entities;

[DebuggerDisplay($"{{{nameof(Name)}}} [{{{nameof(Id)}.ToString()}}]")]
public partial record User
{
    /// <summary>
    /// Refreshes the details from Canvas.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A new <see cref="User"/> instance with the refreshed details.</returns>
    /// <remarks>
    /// This method will return a new instance of the <see cref="User"/>, it does not modify
    /// the existing instance.
    /// </remarks>
    public async Task<User> Refresh(CancellationToken cancellationToken)
    {
        var refreshed = await _client
                .Users
                .Retrieve(Id, cancellationToken)
                .ConfigureAwait(false)
            ?? throw new CanvasClientException("Unable to refresh user: Canvas returned a not found result");
        return refreshed;
    }
}
