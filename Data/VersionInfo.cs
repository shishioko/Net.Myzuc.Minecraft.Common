using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public struct VersionInfo
    {
        [JsonPropertyName("name")]
        public string Name = string.Empty;
        [JsonPropertyName("protocol")]
        public int Protocol = -1;
        public VersionInfo(string name, int protocol)
        {
            Name = name;
            Protocol = protocol;
        }
    }
}