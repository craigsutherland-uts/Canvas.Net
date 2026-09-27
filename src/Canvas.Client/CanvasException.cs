using Canvas.Client.Entities;
using System.Collections.ObjectModel;

namespace Canvas.Client;

/// <summary>
/// An exception from Canvas.
/// </summary>
public class CanvasException
    : ConnectionException
{
    /// <summary>
    /// Initialize a new <see cref="ConnectionException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    /// <param name="message">The message.</param>
    /// <param name="errors">The <see cref="CanvasError"/> instances from Canvas.</param>
    public CanvasException(string url, string? message, IEnumerable<CanvasError> errors)
        : base(url, message, innerException: null)
    {
        Errors = new ReadOnlyCollection<CanvasError>([.. errors]);
    }

    /// <summary>
    /// Initialize a new <see cref="ConnectionException"/> instance.
    /// </summary>
    /// <param name="url">The URL called.</param>
    /// <param name="message">The message.</param>
    /// <param name="errors">The <see cref="CanvasError"/> instances from Canvas.</param>
    /// <param name="innerException">An inner <see cref="Exception"/> instance.</param>
    public CanvasException(string url, string? message, IEnumerable<CanvasError> errors, Exception? innerException)
        : base(url, message, innerException)
    {
        Errors = new ReadOnlyCollection<CanvasError>([.. errors]);
    }

    /// <summary>
    /// The errors from Canvas.
    /// </summary>
    public IReadOnlyCollection<CanvasError> Errors { get; }
}