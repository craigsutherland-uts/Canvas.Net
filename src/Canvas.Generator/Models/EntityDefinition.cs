using Humanizer;

namespace Canvas.Generator.Models;

/// <summary>
/// An entity in the API.
/// </summary>
public class EntityDefinition
{
    /// <summary>
    /// An optional client that will be associated with the entity.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    /// The custom properties for an entity.
    /// </summary>
    public IDictionary<string, EntityPropertyDefinition> Properties { get; } 
        = new Dictionary<string, EntityPropertyDefinition>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Attempts to find a property by its name.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <returns>The <see cref="EntityPropertyDefinition"/> instance if found; <c language="">null</c> otherwise.</returns>
    public EntityPropertyDefinition? FindProperty(string name)
    {
        return Properties.TryGetValue(name, out var property)
            ? property
            : null;
    }
}