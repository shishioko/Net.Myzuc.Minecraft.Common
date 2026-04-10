using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters
{
    internal class ChatComponentJsonConverter : JsonConverter<ChatComponent>
    {
        public override ChatComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonDocument json = JsonDocument.ParseValue(ref reader);
            ChatComponent? result = json.RootElement.ValueKind switch
            {
                JsonValueKind.Array => new TextChatComponent()
                {
                    Children = json.RootElement.EnumerateArray()
                                   .Select(
                                       entry =>
                                       {
                                           JsonDocument entryJson = JsonDocument.Parse(entry.GetRawText());
                                           return entryJson.Deserialize<ChatComponent>(options) ?? throw new SerializationException();
                                       }
                                   )
                },
                JsonValueKind.String => new TextChatComponent(json.RootElement.GetString()!),
                _ => json.RootElement switch
                {
                    var root when root.TryGetProperty("type", out JsonElement type) => type.GetString() switch
                    {
                        "text" => json.Deserialize<TextChatComponent>(options),
                        "translatable" => json.Deserialize<TranslatableChatComponent>(options),
                        "keybind" => json.Deserialize<KeybindChatComponent>(options),
                        "object" => json.Deserialize<ObjectChatComponent>(options),
                        _ => throw new SerializationException()
                    },
                    var root when root.TryGetProperty("text", out _) => json.Deserialize<TextChatComponent>(options),
                    var root when root.TryGetProperty("translate", out _) => json.Deserialize<TranslatableChatComponent>(options),
                    var root when root.TryGetProperty("keybind", out _) => json.Deserialize<KeybindChatComponent>(options),
                    var root when root.TryGetProperty("object", out _) => json.Deserialize<ObjectChatComponent>(options),
                    _ => json.Deserialize<TextChatComponent>(options),
                },
            };
            return result ?? throw new SerializationException();
        }
        public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
        {
            JsonDocument json = JsonSerializer.SerializeToDocument(value, value.GetType(), options);
            json.WriteTo(writer);
        }
    }
}