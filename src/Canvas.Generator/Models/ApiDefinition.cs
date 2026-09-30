namespace Canvas.Generator.Models;

/// <summary>
/// Defines the API that will be generated.
/// </summary>
public sealed class ApiDefinition
{
    /// <summary>
    /// The entities in the API.
    /// </summary>
    public IList<EntityDefinition> Entities { get; } = [];

    /// <summary>
    /// Defines the namespaces for the target project.
    /// </summary>
    public NamespacesDefinition Namespaces { get; set; } = new();

    /// <summary>
    /// Defines a type alias.
    /// </summary>
    /// <remarks>
    /// In the Canvas definitions, there are some type schemas that refer to the same conceptual entities
    /// (e.g., UserNullable and User). These aliases allow the code generator to handle these scenarios.
    /// </remarks>
    public IDictionary<string, string> TypeAliases { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
}
