using Canvas.Client.Entities;
using System.Collections.ObjectModel;

namespace Canvas.Client;

/// <summary>
/// An exception from Canvas.
/// </summary>
/// <remarks>
/// Initialize a new <see cref="ConnectionException"/> instance.
/// </remarks>
/// <param name="url">The URL called.</param>
/// <param name="message">The message.</param>
/// <param name="errors">The <see cref="CanvasError"/> instances from Canvas.</param>
/// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
public class CanvasException(string url, string? message, IEnumerable<CanvasError> errors, Exception? innerException)
        : ConnectionException(url, message, innerException)
{
    /// <summary>
    /// Initialize a new <see cref="ConnectionException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    /// <param name="message">The message.</param>
    /// <param name="errors">The <see cref="CanvasError"/> instances from Canvas.</param>
    public CanvasException(string url, string? message, IEnumerable<CanvasError> errors)
        : this(url, message, errors, innerException: null)
    {
    }

    /// <summary>
    /// The errors from Canvas.
    /// </summary>
    public IReadOnlyCollection<CanvasError> Errors { get; } = new ReadOnlyCollection<CanvasError>([.. errors]);
}