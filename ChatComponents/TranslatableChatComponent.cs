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
        public Identifier TranslationKey { get; set; } = new();
        [JsonPropertyName("with")]
        public IList<ChatComponent>? Arguments { get; set; } = null;
        [JsonPropertyName("fallback")]
        public string? Fallback { get; set; } = null;
        
        public TranslatableChatComponent(Identifier translationKey) : base()
        {
            TranslationKey = translationKey;
        } 
        internal TranslatableChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            TranslationKey = new(nbt["translate"].Get<StringNbtTag>());
            if (nbt.ContainsKey("with"))
            {
                Arguments = nbt["with"].Get<ListNbtTag>().Select(Nbt.Nbt.FromNbt<ChatComponent>).ToList();
            }
            if (nbt.ContainsKey("fallback"))
            {
                Fallback = nbt["fallback"].Get<StringNbtTag>();
            }
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["translate"] = Nbt.Nbt.ToNbt(TranslationKey);
            if (Arguments is not null)
            {
                nbt["with"] = new ListNbtTag(Arguments.Select(Nbt.Nbt.ToNbt).ToList());
            }
            if (Fallback is not null) nbt["fallback"] = (StringNbtTag)Fallback;
            return nbt;
        }
    }
}