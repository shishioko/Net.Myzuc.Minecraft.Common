using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class PlayersInfo
    {
        [JsonPropertyName("max")]
        public int Maximum = 0;
        [JsonPropertyName("online")]
        public int Online = 0;
        [JsonPropertyName("sample")]
        public IList<PlayerSample>? Samples = null;
        public PlayersInfo()
        {
            
        }
    }
}