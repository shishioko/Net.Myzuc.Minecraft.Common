using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public readonly record struct Status
    {
        [JsonPropertyName("version")]
        public VersionInfo? Version { get; init; } = null;
        [JsonPropertyName("players")]
        public PlayersInfo? Players { get; init; } = null;
        [JsonPropertyName("description")]
        public ChatComponent? Description { get; init; } = null;
        [JsonPropertyName("favicon")]
        public string? Icon { get; init; } = null;
        [JsonPropertyName("enforcesSecureChat")]
        public bool? EnforcesSecureChat { get; init; } = null;
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