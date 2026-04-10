using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Data.Primitives;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract record ChatComponent
    {
        [JsonInclude]
        [JsonPropertyName("type")]
        protected abstract string Type { get; }
        [JsonPropertyName("extra")]
        public IReadOnlyList<ChatComponent>? Children { get; set; } = null;
        [JsonPropertyName("color")]
        public ChatColor? Color { get; init; } = null;
        [JsonPropertyName("font")]
        public string? Font { get; init; } = null;
        [JsonPropertyName("bold")]
        public bool? Bold { get; init; } = null;
        [JsonPropertyName("italic")]
        public bool? Italic { get; init; } = null;
        [JsonPropertyName("underlined")]
        public bool? Underlined { get; init; } = null;
        [JsonPropertyName("strikethrough")]
        public bool? Strikethrough { get; init; } = null;
        [JsonPropertyName("obfuscated")]
        public bool? Obfuscated { get; init; } = null;
        [JsonPropertyName("shadow_color")]
        public Color? ShadowColor { get; init; } = null;
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
                Children = data.Select<string, ChatComponent>(child => new TextChatComponent(child)).ToList(),
            };
        }
    }
}