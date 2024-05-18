using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrankIS.ClockifyManagement.Services.Json;
internal class ClockifyDateConverter : JsonConverter<DateTime>
{
    public static string DateFormat => "yyyy-MM-ddTHH:mm:ssZ";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime().ToString(DateFormat));
    }
}
