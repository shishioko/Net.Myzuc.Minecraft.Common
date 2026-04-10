using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record PlayerObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "player";
        [JsonRequired]
        [JsonPropertyName("player")]
        public UnresolvedProfile Player { get; init; }
        [JsonPropertyName("hat")]
        public bool DisplayHat { get; init; } = true;
        public PlayerObjectChatComponent(UnresolvedProfile player)
        {
            Player = player;
        }
    }
}