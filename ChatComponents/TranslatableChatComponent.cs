using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class TranslatableChatComponent : ChatComponent
    {
        [JsonPropertyName("translate")]
        public string TranslationKey;
        [JsonPropertyName("with")]
        public IEnumerable<ChatComponent>? Arguments = null;
        [JsonPropertyName("fallback")]
        public string? Fallback = null;
        public TranslatableChatComponent(string translationKey) : base("translatable")
        {
            TranslationKey = translationKey;
        }  
    }
}