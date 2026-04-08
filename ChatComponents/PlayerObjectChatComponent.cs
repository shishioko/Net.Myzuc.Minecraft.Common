using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class PlayerObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "player";
        [JsonRequired]
        [JsonPropertyName("player")] public RenderProfile Player { get; set; }
        [JsonPropertyName("hat")] public bool DisplayHat { get; set; } = true;
        public PlayerObjectChatComponent(RenderProfile player)
        {
            Player = player;
        }
    }
}