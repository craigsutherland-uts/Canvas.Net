using System.Text.Json.Serialization;

namespace Canvas.Client.Dtos;
/// <summary>
/// A DTO for transferring Lock info entities.
/// </summary>
internal sealed partial record LockInfoDto
{
    [JsonPropertyName("asset_string")]
    public string? AssetString { get; init; }

    [JsonPropertyName("context_module")]
    public string? ContextModule { get; init; }

    [JsonPropertyName("lock_at")]
    public DateTime? LockAt { get; init; }

    [JsonPropertyName("manually_locked")]
    public bool? ManuallyLocked { get; init; }

    [JsonPropertyName("unlock_at")]
    public DateTime? UnlockAt { get; init; }
}