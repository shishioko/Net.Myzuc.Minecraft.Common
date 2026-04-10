using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly record struct PlayersInfo
    {
        [JsonPropertyName("max")]
        public int Maximum { get; init; } = 0;
        [JsonPropertyName("online")]
        public int Online { get; init; } = 0;
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
            Samples = null;
        }
    }
}