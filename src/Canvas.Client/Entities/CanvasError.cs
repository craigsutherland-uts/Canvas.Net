namespace Canvas.Client.Entities;

/// <summary>
/// An error from Canvas.
/// </summary>
public sealed record CanvasError
{
    /// <summary>
    /// The error attribute.
    /// </summary>
    public string? Attribute { get; init; }

    /// <summary>
    /// The error type.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// The error message.
    /// </summary>
    public string? Message { get; init; }
}