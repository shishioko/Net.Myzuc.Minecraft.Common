using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class AtlasObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "atlas";
        [JsonRequired]
        [JsonPropertyName("atlas")]
        public string Atlas { get; set; }
        [JsonRequired]
        [JsonPropertyName("sprite")]
        public string Sprite { get; set; }
        public AtlasObjectChatComponent(string atlas, string sprite)
        {
            Atlas = atlas;
            Sprite = sprite;
        }
    }
}