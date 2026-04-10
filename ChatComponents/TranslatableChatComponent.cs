using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record TranslatableChatComponent : ChatComponent
    {
        protected override string Type => "translatable";
        [JsonRequired]
        [JsonPropertyName("translate")]
        public string TranslationKey { get; init; }
        [JsonPropertyName("with")]
        public IEnumerable<ChatComponent>? Arguments { get; init; } = null;
        [JsonPropertyName("fallback")]
        public string? Fallback { get; init; } = null;
        public TranslatableChatComponent(string translationKey) : base()
        {
            TranslationKey = translationKey;
        }  
    }
}