using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data.Primitives.JsonConverters
{
    internal sealed class ChatColorJsonConverter : JsonConverter<ChatColor>
    {
        public override ChatColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new(reader.GetString() ?? throw new SerializationException());
        }
        public override void Write(Utf8JsonWriter writer, ChatColor value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}