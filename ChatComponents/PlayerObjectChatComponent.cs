using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Objects;

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
        internal override void Serialize(JsonObject json)
        {
            json["player"] = JsonSerializer.SerializeToNode(Player, Global.JsonSerializerOptions); //todo: fix
            json["hat"] = DisplayHat;
            base.Serialize(json);
        }
    }
}