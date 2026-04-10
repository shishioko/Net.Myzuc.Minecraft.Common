using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record TranslatableChatComponent : ChatComponent
    {
        protected override string Type => "translatable";
        [JsonRequired]
        [JsonPropertyName("translate")]
        public Identifier TranslationKey { get; init; }
        [JsonPropertyName("with")]
        public IReadOnlyList<ChatComponent>? Arguments { get; init; } = null;
        [JsonPropertyName("fallback")]
        public string? Fallback { get; init; } = null;
        public TranslatableChatComponent(Identifier translationKey) : base()
        {
            TranslationKey = translationKey;
        } 
        
        internal TranslatableChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            TranslationKey = new(nbt["translate"].Get<StringNbtTag>());
            if (nbt.ContainsKey("with"))
            {
                Arguments = nbt["with"].Get<ListNbtTag>().Select(Deserialize).ToList();
            }
            if (nbt.ContainsKey("fallback"))
            {
                Fallback = nbt["fallback"].Get<StringNbtTag>();
            }
        }

        internal override CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = base.Serialize();
            nbt["translate"] = TranslationKey.Serialize();
            if (Arguments is not null)
            {
                nbt["with"] = new ListNbtTag(Arguments.Select(entry => entry.Serialize()).ToList());
            }
            if (Fallback is not null) nbt["fallback"] = (StringNbtTag)Fallback;
            return nbt;
        }
    }
}