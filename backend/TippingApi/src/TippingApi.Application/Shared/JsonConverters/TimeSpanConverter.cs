using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TippingApi.Application.Shared.JsonConverters;

public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    private const string Format = @"hh\:mm";

    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString();
        if (TimeSpan.TryParseExact(s, "c", CultureInfo.InvariantCulture, out var ts) ||
            TimeSpan.TryParseExact(s, @"hh\:mm", CultureInfo.InvariantCulture, out ts))
        {
            return ts;
        }

        throw new JsonException($"Invalid TimeSpan format: {s}");
    }


    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}