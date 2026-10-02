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

    /// <summary>
    /// The root-level course functionality.
    /// </summary>
    ICourses Courses { get; }

    /// <summary>
    /// The root-level user functionality.
    /// </summary>
    IUsers Users { get; }
}
