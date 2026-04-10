using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class PlayerObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "player";
        [JsonRequired]
        [JsonPropertyName("player")]
        public UnresolvedProfile Player { get; set; }
        [JsonPropertyName("hat")]
        public bool DisplayHat { get; set; } = true;
        public PlayerObjectChatComponent(UnresolvedProfile player)
        {
            Player = player;
        }
    }
}