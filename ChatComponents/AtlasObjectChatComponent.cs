using System.Text.Json.Nodes;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class AtlasObjectChatComponent : ObjectChatComponent
    {
        public string? Atlas = null;
        public string Sprite;
        public AtlasObjectChatComponent(string sprite) : base("atlas")
        {
            Sprite = sprite;
        }
        internal override void Serialize(JsonObject json)
        {
            if (Atlas is not null) json["atlas"] = Atlas;
            json["sprite"] = Sprite;
            base.Serialize(json);
        }
    }
}