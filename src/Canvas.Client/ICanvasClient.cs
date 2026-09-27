using Canvas.Client.Interfaces;

namespace Canvas.Client;

/// <summary>
/// A client that connects to a Canvas instance.
/// </summary>
public interface ICanvasClient
{
    /// <summary>
    /// The root-level account functionality.
    /// </summary>
    IAccounts Accounts { get; }
}
