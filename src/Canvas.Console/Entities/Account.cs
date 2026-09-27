using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Canvas.Entities;
/// <summary>
///The Account entity definition.
/// </summary>
[DebuggerDisplay($"{{{nameof(Name)}}}")]
public sealed partial record Account
{
    public int? DefaultGroupStorageQuotaMb { get; init; }
    public int? DefaultStorageQuotaMb { get; init; }
    public string? DefaultTimeZone { get; init; }
    public int? DefaultUserStorageQuotaMb { get; init; }
    public int? Id { get; init; }
    public string? IntegrationId { get; init; }
    public string? LtiGuid { get; init; }
    /// <summary>
    /// The name.
    /// </summary>
    /// <example>Bob</example>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    public int? ParentAccountId { get; init; }
    public int? RootAccountId { get; init; }
    public string? SisAccountId { get; init; }
    public int? SisImportId { get; init; }
    public string? Uuid { get; init; }
    public string? WorkflowState { get; init; }
}
