using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Objects.JsonConverters
{
    internal class ChatComponentJsonConverter : JsonConverter<ChatComponent>
    {
        public override ChatComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonDocument json = JsonDocument.ParseValue(ref reader);
            if (json.RootElement.ValueKind == JsonValueKind.Array)
            {
                return new TextChatComponent()
                {
                    Children = json.RootElement.EnumerateArray().Select(
                        entry =>
                        {
                            JsonDocument entryJson = JsonDocument.Parse(entry.GetRawText());
                            return entryJson.Deserialize<ChatComponent>(options) ?? throw new InvalidDataException();
                        })
                };
            }
            if (json.RootElement.ValueKind == JsonValueKind.String)
            {
                return new TextChatComponent(json.RootElement.GetString()!);
            }
            string? type = json.RootElement.GetProperty("type").GetString();
            ChatComponent? result = null;
            if (type == "text" || json.RootElement.TryGetProperty("text", out _) || type is null)
            {
                result = json.Deserialize<TextChatComponent>(options);
            }
            else if (type == "translatable" || json.RootElement.TryGetProperty("translate", out _))
            {
                result = json.Deserialize<TranslatableChatComponent>(options);
            }
            else if (type == "keybind" || json.RootElement.TryGetProperty("keybind", out _))
            {
                result = json.Deserialize<KeybindChatComponent>(options);
            }
            else if (type == "object" || json.RootElement.TryGetProperty("object", out _))
            {
                string? @object = json.RootElement.GetProperty("object").GetString();
                result = @object switch
                {
                    null or "atlas" => json.Deserialize<AtlasObjectChatComponent>(options),
                    "player" => json.Deserialize<PlayerObjectChatComponent>(options),
                    _ => result
                };
            }
            return result ?? throw new InvalidDataException();
        }

        public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
        {
            JsonDocument json = JsonSerializer.SerializeToDocument(value, value.GetType(), options);
            json.WriteTo(writer);
        }
    }
}