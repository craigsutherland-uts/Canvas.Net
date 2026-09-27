namespace Canvas.Client;

/// <summary>
/// A general error has occurred in the <see cref="ICanvasConnection"/>.
/// </summary>
/// <remarks>
/// Initialize a new <see cref="ConnectionException"/> instance.
/// </remarks>
/// <param name="url">The URL called.</param>
/// <param name="message">The message.</param>
/// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
public class ConnectionException(string url, string? message, Exception? innerException)
        : Exception(message, innerException)
{
    /// <summary>
    /// Initialize a new <see cref="ConnectionException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    /// <param name="message">The message.</param>
    public ConnectionException(string url, string? message)
        : this(url, message, innerException: null)
    {
    }

    /// <summary>
    /// The returned content from the call.
    /// </summary>
    public string? Content { get; init; }

    /// <summary>
    /// The URL called.
    /// </summary>
    public string Url { get; } = url;
}
