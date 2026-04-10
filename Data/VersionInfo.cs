using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public record struct VersionInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("protocol")]
        public int Protocol { get; init; } = -1;
        public VersionInfo()
        {
            
        }
        public VersionInfo(int protocol)
        {
            Protocol = protocol;
        }
        public VersionInfo(string name, int protocol)
        {
            Name = name;
            Protocol = protocol;
        }
    }
}