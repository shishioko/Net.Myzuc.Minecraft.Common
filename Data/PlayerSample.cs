using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public struct PlayerSample
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("id")] public Guid Guid { get; set; } = Guid.Empty;
        public PlayerSample(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}