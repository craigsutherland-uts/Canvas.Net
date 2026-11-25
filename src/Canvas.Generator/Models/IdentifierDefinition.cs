namespace Canvas.Generator.Models;

/// <summary>
/// The definition of an identifier type.
/// </summary>
public sealed class IdentifierDefinition
{
    /// <summary>
    /// The underlying type for the identifier.
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    /// The default values for the identifier type.
    /// </summary>
    public IDictionary<string, string> Values { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
}
