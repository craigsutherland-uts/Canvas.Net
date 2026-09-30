using Humanizer;

namespace Canvas.Generator.Models;

/// <summary>
/// An entity in the API.
/// </summary>
public class EntityDefinition
{
    private Dictionary<string, EntityPropertyDefinition>? propertiesByJsonName;
    private Dictionary<string, EntityPropertyDefinition>? propertiesByCSharpName;

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
    public IList<EntityPropertyDefinition> Properties { get; } = [];

    /// <summary>
    /// Attempts to find a property by its name.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <returns>The <see cref="EntityPropertyDefinition"/> instance if found; <c language="">null</c> otherwise.</returns>
    public EntityPropertyDefinition? FindProperty(string name)
    {
        propertiesByJsonName  ??= Properties.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
        propertiesByCSharpName ??= Properties.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
        if (!propertiesByJsonName.TryGetValue(name, out var property))
        {
            propertiesByCSharpName.TryGetValue(name.Dehumanize(), out property);
        }

        return property;
    }
}