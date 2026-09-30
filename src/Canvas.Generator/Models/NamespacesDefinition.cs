namespace Canvas.Generator.Models;

/// <summary>
/// The namespaces for a project.
/// </summary>
public sealed class NamespacesDefinition
{
    /// <summary>
    /// The clients namespace.
    /// </summary>
    public string Clients { get; set; } = "Interfaces";

    /// <summary>
    /// The DTOs namespace.
    /// </summary>
    public string Dtos { get; set; } = "Dtos";

    /// <summary>
    /// The entities namespace.
    /// </summary>
    public string Entities { get; set; } = "Entities";
}