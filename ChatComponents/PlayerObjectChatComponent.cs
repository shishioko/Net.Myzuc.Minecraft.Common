using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class PlayerObjectChatComponent : ObjectChatComponent
    {
        [JsonPropertyName("player")]
        public RenderProfile Player;
        [JsonPropertyName("hat")]
        public bool DisplayHat = true;
        public PlayerObjectChatComponent(RenderProfile player) : base("player")
        {
            Player = player;
        }
    }
}