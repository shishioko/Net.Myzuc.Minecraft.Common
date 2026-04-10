using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record AtlasObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "atlas";
        [JsonRequired]
        [JsonPropertyName("atlas")]
        public string Atlas { get; init; }
        [JsonRequired]
        [JsonPropertyName("sprite")]
        public string Sprite { get; init; }
        public AtlasObjectChatComponent(string atlas, string sprite)
        {
            Atlas = atlas;
            Sprite = sprite;
        }
    }
}