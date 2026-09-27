namespace Canvas.Client;

/// <summary>
/// An exception for when the user is unauthorised.
/// </summary>
/// <remarks>
/// Initialize a new <see cref="UnauthorizedException"/> instance.
/// </remarks>
/// <param name="url">The URL called.</param>
/// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
public class UnauthorizedException(string url, Exception? innerException)
        : ConnectionException(url, "The connection has not been authorised.", innerException)
{
    /// <summary>
    /// Initialise a new <see cref="UnauthorizedException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    public UnauthorizedException(string url)
        : this(url, innerException: null)
    {
    }
}
