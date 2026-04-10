using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.ChatComponents.NbtConverters;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [NbtConverter(typeof(ChatComponentNbtConverter))]
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract class ChatComponent
    {
        [JsonInclude] [NbtInclude]
        [JsonPropertyName("type")] [NbtProperty("type")]
        protected abstract string Type { get; }
        [JsonPropertyName("extra")] [NbtProperty("extra")]
        public IEnumerable<ChatComponent>? Children { get; set; } = null;
        [JsonConverter(typeof(ChatColorJsonConverter))] [NbtConverter(typeof(ChatColorNbtConverter))]
        [JsonPropertyName("color")] [NbtProperty("color")]
        public Color? Color { get; set; } = null;
        [JsonPropertyName("font")] [NbtProperty("font")]
        public string? Font { get; set; } = null;
        [JsonPropertyName("bold")] [NbtProperty("bold")]
        public bool? Bold { get; set; } = null;
        [JsonPropertyName("italic")] [NbtProperty("italic")]
        public bool? Italic { get; set; } = null;
        [JsonPropertyName("underlined")] [NbtProperty("underlined")]
        public bool? Underlined { get; set; } = null;
        [JsonPropertyName("strikethrough")] [NbtProperty("strikethrough")]
        public bool? Strikethrough { get; set; } = null;
        [JsonPropertyName("obfuscated")] [NbtProperty("obfuscated")]
        public bool? Obfuscated { get; set; } = null;
        [JsonConverter(typeof(ColorNbtJsonConverter))]
        [JsonPropertyName("shadow_color")] [NbtProperty("shadow_color")]
        public Color? ShadowColor { get; set; } = null;
        protected internal ChatComponent()
        {
            
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonSerializerOptions.Default);
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