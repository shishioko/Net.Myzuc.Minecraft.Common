using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract class ChatComponent
    {
        [JsonInclude]
        [JsonPropertyName("type")] protected abstract string Type { get; }
        [JsonPropertyName("extra")] public IEnumerable<ChatComponent>? Children { get; set; } = null;
        [JsonConverter(typeof(ChatColorJsonSerializer))]
        [JsonPropertyName("color")] public Color? Color { get; set; } = null;
        [JsonPropertyName("font")] public string? Font { get; set; } = null;
        [JsonPropertyName("bold")] public bool? Bold { get; set; } = null;
        [JsonPropertyName("italic")] public bool? Italic { get; set; } = null;
        [JsonPropertyName("underlined")] public bool? Underlined { get; set; } = null;
        [JsonPropertyName("strikethrough")] public bool? Strikethrough { get; set; } = null;
        [JsonPropertyName("obfuscated")] public bool? Obfuscated { get; set; } = null;
        [JsonConverter(typeof(ColorNbtJsonSerializer))]
        [JsonPropertyName("shadow_color")] public Color? ShadowColor { get; set; } = null;
        protected internal ChatComponent()
        {
            
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Global.JsonSerializerOptions);
        }
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
        public static implicit operator ChatComponent(ChatComponent[] data)
        {
            return new TextChatComponent()
            {
                Children = data,
            };
        }
        public static implicit operator ChatComponent(string[] data)
        {
            return new TextChatComponent()
            {
                Children = data.Select(child => new TextChatComponent(child)),
            };
        }
    }
}