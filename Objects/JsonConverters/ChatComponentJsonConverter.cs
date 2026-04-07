using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Objects.JsonConverters
{
    internal class ChatComponentJsonConverter : JsonConverter<ChatComponent>
    {
        public override ChatComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
        {
            JsonObject json = new();
            value.Serialize(json);
            json.WriteTo(writer, options);
        }
    }
}