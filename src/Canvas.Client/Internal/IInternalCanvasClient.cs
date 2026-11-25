namespace Canvas.Client.Internal;

/// <summary>
/// An extended version of <see cref="ICanvasClient"/> for internal use.
/// </summary>
public interface IInternalCanvasClient
    : ICanvasClient
{
    /// <summary>
    /// Expose the underlying <see cref="IServiceProvider"/> instance.
    /// </summary>
    IServiceProvider Services { get; }
}
