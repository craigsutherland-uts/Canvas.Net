using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring User entities.
/// </summary>
internal sealed partial record UserDto
{
    [JsonPropertyName("avatar_state")]
    public string? AvatarState { get; init; }

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; init; }
    public string? Bio { get; init; }
    public string? Email { get; init; }
    public IList<EnrollmentDto>? Enrollments { get; init; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; init; }
    public int? Id { get; init; }

    [JsonPropertyName("integration_id")]
    public string? IntegrationId { get; init; }

    [JsonPropertyName("last_login")]
    public DateTime? LastLogin { get; init; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }
    public string? Locale { get; init; }

    [JsonPropertyName("login_id")]
    public string? LoginId { get; init; }
    public string? Name { get; init; }

    [JsonPropertyName("short_name")]
    public string? ShortName { get; init; }
    public string? Sections { get; init; }

    [JsonPropertyName("sis_import_id")]
    public int? SisImportId { get; init; }

    [JsonPropertyName("sis_user_id")]
    public string? SisUserId { get; init; }

    [JsonPropertyName("sortable_name")]
    public string? SortableName { get; init; }

    [JsonPropertyName("time_zone")]
    public string? TimeZone { get; init; }
}