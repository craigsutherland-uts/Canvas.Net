namespace Canvas.Client;

/// <summary>
/// A connection to a Canvas instance.
/// </summary>
public interface ICanvasConnection
{
    /// <summary>
    /// Perform a GET operation and deserialise the response for a single entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to deserialise.</typeparam>
    /// <param name="url">The URL to GET.</param>
    /// <param name="options">The options to pass to the URL.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The deserialised entity if valid; <see langword="null"/> otherwise.</returns>
    Task<TEntity?> GetEntity<TEntity>(string url, CanvasOptions options, CancellationToken cancellationToken)
        where TEntity : class;

    /// <summary>
    /// Perform a GET operation and deserialise the response.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to deserialise.</typeparam>
    /// <param name="url">The URL to GET.</param>
    /// <param name="options">The options to pass to the URL.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The deserialised entity if valid; <see langword="null"/> otherwise.</returns>
    IAsyncEnumerable<TEntity?> ListEntities<TEntity>(string url, CanvasOptions options, CancellationToken cancellationToken)
        where TEntity : class;
}
