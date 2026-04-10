using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class TranslatableChatComponent : ChatComponent
    {
        protected override string Type => "translatable";
        [JsonRequired]
        [JsonPropertyName("translate")]
        public string TranslationKey { get; set; }
        [JsonPropertyName("with")]
        public IEnumerable<ChatComponent>? Arguments { get; set; } = null;
        [JsonPropertyName("fallback")]
        public string? Fallback { get; set; } = null;
        public TranslatableChatComponent(string translationKey) : base()
        {
            TranslationKey = translationKey;
        }  
    }
}