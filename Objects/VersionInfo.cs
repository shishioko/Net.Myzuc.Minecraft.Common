using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public struct VersionInfo
    {
        [JsonPropertyName("name")]
        public string Name = Minecraft.Version.ToString();
        [JsonPropertyName("protocol")]
        public int Protocol = Minecraft.ProtocolVersion;
        public VersionInfo(string name, int protocol)
        {
            Name = name;
            Protocol = protocol;
        }
    }
}