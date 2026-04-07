using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public class PlayersInfo
    {
        [JsonPropertyName("max")] public int Maximum { get; set; } = 0;
        [JsonPropertyName("online")] public int Online { get; set; } = 0;
        [JsonPropertyName("sample")] public IList<PlayerSample>? Samples { get; set; } = null;
        public PlayersInfo(int maximum = 0, int online = 0)
        {
            Maximum = maximum;
            Online = online;
        }
    }
}