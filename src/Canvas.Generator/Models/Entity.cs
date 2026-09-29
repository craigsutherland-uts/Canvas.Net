namespace Canvas.Generator.Models;

/// <summary>
/// An entity in the API.
/// </summary>
public class Entity
{
    /// <summary>
    /// An optional client that will be associated with the entity.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    /// The name of the entity.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The custom properties for an entity.
    /// </summary>
    public IList<EntityProperty> Properties { get; } = [];
}