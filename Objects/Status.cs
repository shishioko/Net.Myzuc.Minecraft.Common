using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version = null;
        [JsonPropertyName("players")]
        public PlayersInfo? Players = null;
        [JsonPropertyName("description")]
        public string? Description = null;
        [JsonPropertyName("favicon")]
        public string? Icon = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? EnforcesSecureChat = null;
        public Status(string? description = null, VersionInfo? version = null)
        {
            Description = description;
            Version = version;
        }
    }
}