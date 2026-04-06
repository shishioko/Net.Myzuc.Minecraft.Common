using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public struct PlayerSample
    {
        [JsonPropertyName("name")]
        public string Name;
        [JsonPropertyName("id")]
        public Guid Guid;
        public PlayerSample(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}