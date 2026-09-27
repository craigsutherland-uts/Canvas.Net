using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Canvas.Client.Implementations;

/// <summary>
/// Converts to and from the datetime format that Canvas expects.
/// </summary>
internal sealed class CanvasDateTimeConverter
    : JsonConverter<DateTime>
{
    public const string CanvasDateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    /// <summary>
    /// Converts from a string to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="reader">The reader to use.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The serialisation options.</param>
    /// <returns>The converted <see cref="DateTime"/>.</returns>
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return string.IsNullOrEmpty(value)
            ? DateTime.MinValue
            : (DateTime.TryParseExact(
                value,
                CanvasDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal,
                out var result) ? result : DateTime.MinValue);
    }

    /// <summary>
    /// Write a <see cref="DateTime"/> to the writer.
    /// </summary>
    /// <param name="writer">The writer to use.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">The serialisation options.</param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CanvasDateFormat, CultureInfo.InvariantCulture));
    }
}
