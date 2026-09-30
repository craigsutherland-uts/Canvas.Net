namespace Canvas.Generator.Models;

/// <summary>
/// A property for an entity.
/// </summary>
public class EntityPropertyDefinition
{
    /// <summary>
    /// An alias for the property.
    /// </summary>
    public string? Alias {  get; set; }

    /// <summary>
    /// A static method to convert from a raw value to the type value.
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    /// The type that contains the static method.
    /// </summary>
    public string? FromType { get; set; }

    /// <summary>
    /// The name of the entity.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Is the property nullable.
    /// </summary>
    public bool Nullable { get; set; } = true;

    /// <summary>
    /// An optional value to use if the raw value is null.
    /// </summary>
    public string? NullValue { get; set; }

    /// <summary>
    /// Should the generator skip this property.
    /// </summary>
    public bool Skip {  get; set; }

    /// <summary>
    /// The type of the entity.
    /// </summary>
    public string? Type { get; set; }
}