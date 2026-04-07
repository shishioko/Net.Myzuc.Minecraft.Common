using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class AtlasObjectChatComponent : ObjectChatComponent
    {
        [JsonPropertyName("atlas")] public string? Atlas { get; set; } = null;
        [JsonPropertyName("sprite")] public string Sprite { get; set; }
        public AtlasObjectChatComponent(string sprite) : base("atlas")
        {
            Sprite = sprite;
        }
    }
}