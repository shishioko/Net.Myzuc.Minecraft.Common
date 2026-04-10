using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class TranslatableChatComponent : ChatComponent
    {
        protected override string Type => "translatable";
        [JsonRequired] [NbtRequired]
        [JsonPropertyName("translate")] [NbtProperty("translate")]
        public string TranslationKey { get; set; }
        [JsonPropertyName("with")] [NbtProperty("with")]
        public IEnumerable<ChatComponent>? Arguments { get; set; } = null;
        [JsonPropertyName("fallback")] [NbtProperty("fallback")]
        public string? Fallback { get; set; } = null;
        public TranslatableChatComponent(string translationKey) : base()
        {
            TranslationKey = translationKey;
        }  
    }
}