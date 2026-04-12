using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Objects.JsonConverters
{
    internal class ObjectChatComponentJsonConverter : JsonConverter<ObjectChatComponent>
    {
        public override ObjectChatComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonDocument json = JsonDocument.ParseValue(ref reader);
            ObjectChatComponent? result = json.RootElement switch
            {
                var root when root.TryGetProperty("object", out JsonElement type) => type.GetString() switch
                {
                    "atlas" => json.Deserialize<AtlasObjectChatComponent>(options),
                    "player" => json.Deserialize<PlayerObjectChatComponent>(options),
                    _ => throw new SerializationException()
                },
                var root when root.TryGetProperty("atlas", out _) => json.Deserialize<AtlasObjectChatComponent>(options),
                var root when root.TryGetProperty("sprite", out _) => json.Deserialize<AtlasObjectChatComponent>(options),
                var root when root.TryGetProperty("player", out _) => json.Deserialize<PlayerObjectChatComponent>(options),
                _ => json.Deserialize<AtlasObjectChatComponent>(options),
            };
            return result ?? throw new SerializationException();
        }
        public override void Write(Utf8JsonWriter writer, ObjectChatComponent value, JsonSerializerOptions options)
        {
            JsonDocument json = JsonSerializer.SerializeToDocument(value, value.GetType(), options);
            json.WriteTo(writer);
        }
    }
}