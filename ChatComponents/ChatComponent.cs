using System.Drawing;
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
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
    }
}