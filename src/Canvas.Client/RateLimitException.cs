namespace Canvas.Client;

/// <summary>
/// An exception for when the rate limit is exceeded.
/// </summary>
/// <remarks>
/// Initialize a new <see cref="RateLimitException"/> instance.
/// </remarks>
/// <param name="url">The URL called.</param>
/// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
public class RateLimitException(string url, Exception? innerException)
        : ConnectionException(url, "This operation is fobidden due to exceeding the API rate limit.", innerException)
{
    /// <summary>
    /// Initialise a new <see cref="RateLimitException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    public RateLimitException(string url)
        : this(url, innerException: null)
    {
    }
}
