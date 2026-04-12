using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Primitives.JsonConverters
{
    internal sealed class IdentifierJsonConverter : JsonConverter<Identifier>
    {
        public override Identifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new(reader.GetString() ?? throw new SerializationException());
        }
        public override void Write(Utf8JsonWriter writer, Identifier value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.FullIdentifier);
        }
    }
}