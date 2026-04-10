using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public readonly record struct PlayerSample
    {
        [JsonPropertyName("name")] 
        public string Name { get; init; } = string.Empty;
        [JsonConverter(typeof(GuidStringJsonConverter))]
        [JsonPropertyName("id")] 
        public Guid Guid { get; init; } = Guid.Empty;
        [JsonConstructor]
        public PlayerSample()
        {
            
        }
        public PlayerSample(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}