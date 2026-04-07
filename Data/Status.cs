using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public class Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version = null;
        [JsonPropertyName("players")]
        public PlayersInfo? Players = null;
        [JsonPropertyName("description")]
        public ChatComponent? Description = null;
        [JsonPropertyName("favicon")]
        public string? Icon = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? EnforcesSecureChat = null;
        public Status(ChatComponent? description = null, VersionInfo? version = null)
        {
            Description = description;
            Version = version;
        }
    }
}