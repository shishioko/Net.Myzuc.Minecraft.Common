using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record AtlasObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "atlas";
        [JsonRequired]
        [JsonPropertyName("atlas")]
        public string Atlas { get; set; } = "";
        [JsonRequired]
        [JsonPropertyName("sprite")]
        public string Sprite { get; set; } = "";
        
        public AtlasObjectChatComponent(string atlas, string sprite)
        {
            Atlas = atlas;
            Sprite = sprite;
        }
        internal AtlasObjectChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Atlas = nbt["atlas"].Get<StringNbtTag>();
            Sprite = nbt["sprite"].Get<StringNbtTag>();
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["atlas"] = (StringNbtTag)Atlas;
            nbt["sprite"] = (StringNbtTag)Sprite;
            return nbt;
        }
    }
}