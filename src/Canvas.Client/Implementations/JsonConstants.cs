using Canvas_Client;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Canvas.Client.Implementations;

/// <summary>
/// Internal constants to use when serialising and deserialising JSON.
/// </summary>
internal static class JsonConstants
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new CanvasDateTimeConverter(),
            new VogenTypesFactory(),
        },
    };
}
