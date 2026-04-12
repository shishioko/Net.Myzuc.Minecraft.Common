using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record PlayerSample
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; } = string.Empty;
        [JsonConverter(typeof(GuidStringJsonConverter))]
        [JsonPropertyName("id")] 
        public Guid Guid { get; set; } = Guid.Empty;
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