using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Primitives.JsonConverters
{
    internal sealed class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new(reader.GetInt32());
        }
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Argb);
        }
    }
}