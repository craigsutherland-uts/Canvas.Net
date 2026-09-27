namespace Canvas.Client;

/// <summary>
/// An exception for when the operation is forbidden due to insufficient permissions.
/// </summary>
/// <remarks>
/// Initialize a new <see cref="ForbiddenException"/> instance.
/// </remarks>
/// <param name="url">The URL called.</param>
/// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
public class ForbiddenException(string url, Exception? innerException)
        : ConnectionException(url, "This operation is fobidden due to a lack of priviledges.", innerException)
{
    /// <summary>
    /// Initialise a new <see cref="ForbiddenException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    public ForbiddenException(string url)
        : this(url, innerException: null)
    {
    }
}
