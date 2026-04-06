using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version = new();
        [JsonPropertyName("players")]
        public PlayersInfo? Players = new();
        [JsonPropertyName("description")]
        public string? Description = string.Empty;
        [JsonPropertyName("favicon")]
        public string? Icon = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? EnforcesSecureChat = false;
    }
}