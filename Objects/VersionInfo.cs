using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record VersionInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("protocol")]
        public int Protocol { get; set; } = -1;
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