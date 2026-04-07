using System.Drawing;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract class ChatComponent
    {
        [JsonPropertyName("type")]
        private readonly string Type;
        [JsonPropertyName("extra")]
        public IEnumerable<ChatComponent>? Children = null;
        [JsonConverter(typeof(HexColorJsonSerializer))]
        [JsonPropertyName("color")]
        public Color? Color = null;
        [JsonPropertyName("font")]
        public string? Font = null;
        [JsonPropertyName("bold")]
        public bool? Bold = null;
        [JsonPropertyName("italic")]
        public bool? Italic = null;
        [JsonPropertyName("underlined")]
        public bool? Underlined = null;
        [JsonPropertyName("strikethrough")]
        public bool? Strikethrough = null;
        [JsonPropertyName("obfuscated")]
        public bool? Obfuscated = null;
        [JsonConverter(typeof(IntegerColorJsonSerializer))]
        [JsonPropertyName("shadow_color")]
        public Color? ShadowColor = null;
        protected internal ChatComponent(string type)
        {
            Type = type;
        }

        internal virtual void Serialize(JsonObject json)
        {
            json["type"] = Type;
            if (Children is not null)
            {
                json["extra"] = new JsonArray(Children.Select(
                    child =>
                    {
                        JsonObject childJson = new();
                        child.Serialize(childJson);
                        return childJson;
                    }
                ).ToArray());
            }
            if (Color.HasValue) json["color"] = $"#{(Color.Value.ToArgb() & 0x00FFFFFF):X6}";
            if (Font is not null) json["font"] = Font;
            if (Bold.HasValue) json["bold"] = Bold.Value;
            if (Italic.HasValue) json["italic"] = Italic.Value;
            if (Underlined.HasValue) json["underlined"] = Underlined.Value;
            if (Strikethrough.HasValue) json["strikethrough"] = Strikethrough.Value;
            if (Obfuscated.HasValue) json["obfuscated"] = Obfuscated.Value;
            if (ShadowColor.HasValue) json["shadow_color"] = ShadowColor.Value.ToArgb();
            //todo: more stuff
        }
        /*internal static ChatComponent Deserialize(JsonElement json)
        {
            
        }*/
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
    }
}