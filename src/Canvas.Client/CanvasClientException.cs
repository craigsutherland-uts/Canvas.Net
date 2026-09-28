namespace Canvas.Client;

/// <summary>
/// A general error has occurred in a Canvas client.
/// </summary>
/// <param name="message">The message.</param>
/// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
public class CanvasClientException(string? message, Exception? innerException)
        : Exception(message, innerException)
{
    /// <summary>
    /// Initialize a new <see cref="ConnectionException"/> instance.
    /// </summary>
    /// <param name="message">The message.</param>
    public CanvasClientException(string? message)
        : this(message, innerException: null)
    {
    }
}
