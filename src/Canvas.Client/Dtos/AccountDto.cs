using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Account entities.
/// </summary>
internal sealed partial record AccountDto
{
    [JsonPropertyName("default_group_storage_quota_mb")]
    public int? DefaultGroupStorageQuotaMb { get; init; }

    [JsonPropertyName("default_storage_quota_mb")]
    public int? DefaultStorageQuotaMb { get; init; }

    [JsonPropertyName("default_time_zone")]
    public string? DefaultTimeZone { get; init; }

    [JsonPropertyName("default_user_storage_quota_mb")]
    public int? DefaultUserStorageQuotaMb { get; init; }
    public  string ? Id { get; init; }

    [JsonPropertyName("integration_id")]
    public string? IntegrationId { get; init; }

    [JsonPropertyName("lti_guid")]
    public string? LtiGuid { get; init; }
    public string? Name { get; init; }

    [JsonPropertyName("parent_account_id")]
    public  string ? ParentAccountId { get; init; }

    [JsonPropertyName("root_account_id")]
    public  string ? RootAccountId { get; init; }

    [JsonPropertyName("sis_account_id")]
    public string? SisAccountId { get; init; }

    [JsonPropertyName("sis_import_id")]
    public int? SisImportId { get; init; }
    public string? Uuid { get; init; }

    [JsonPropertyName("workflow_state")]
    public string? WorkflowState { get; init; }
}