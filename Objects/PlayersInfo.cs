using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record PlayersInfo
    {
        [JsonPropertyName("max")]
        public int Maximum { get; set; } = 0;
        [JsonPropertyName("online")]
        public int Online { get; set; } = 0;
        [JsonPropertyName("sample")] 
        public IReadOnlyList<PlayerSample>? Samples { get; } = null;
        public PlayersInfo()
        {
            
        }
        public PlayersInfo(int maximum, int online)
        {
            Maximum = maximum;
            Online = online;
            Samples = null;
        }
        public PlayersInfo(int maximum, int online, IReadOnlyList<PlayerSample>? samples)
        {
            Maximum = maximum;
            Online = online;
            Samples = samples;
        }
    }
}