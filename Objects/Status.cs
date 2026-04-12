using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version { get; set; } = null;
        [JsonPropertyName("players")]
        public PlayersInfo? Players { get; set; } = null;
        [JsonPropertyName("description")]
        public ChatComponent? Description { get; set; } = null;
        [JsonPropertyName("favicon")]
        public string? Icon { get; set; } = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? EnforcesSecureChat { get; set; } = null;
        public Status()
        {
            
        }
        public Status(ChatComponent? description)
        {
            Description = description;
        }
        public Status(ChatComponent? description, VersionInfo? version)
        {
            Description = description;
            Version = version;
        }
        public Status(ChatComponent? description, VersionInfo? version, PlayersInfo? players)
        {
            Description = description;
            Version = version;
            Players = players;
        }
    }
}