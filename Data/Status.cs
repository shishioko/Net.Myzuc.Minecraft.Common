using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public class Status
    {
        [JsonPropertyName("version")] public VersionInfo? Version { get; set; } = null;
        [JsonPropertyName("players")] public PlayersInfo? Players { get; set; } = null;
        [JsonPropertyName("description")] public ChatComponent? Description { get; set; } = null;
        [JsonPropertyName("favicon")] public string? Icon { get; set; } = null;
        [JsonPropertyName("enforcesSecureChat")] public bool? EnforcesSecureChat { get; set; } = null;
        public Status(ChatComponent? description = null, VersionInfo? version = null)
        {
            Description = description;
            Version = version;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Global.JsonSerializerOptions);
        }
        public static Status Parse(string data)
        {
            return JsonSerializer.Deserialize<Status>(data, Global.JsonSerializerOptions) ?? throw new InvalidDataException();
        }
    }
}