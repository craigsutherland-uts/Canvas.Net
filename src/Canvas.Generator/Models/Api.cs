namespace Canvas.Generator.Models;

/// <summary>
/// Defines the API that will be generated.
/// </summary>
public sealed class Api
{
    /// <summary>
    /// The entities in the API.
    /// </summary>
    public IList<Entity> Entities { get; } = [];
}
