using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly record struct ResolvedProfile
    {
        [JsonConverter(typeof(GuidStringJsonConverter))]
        [JsonPropertyName("id")]
        public Guid Guid { get; init; } = Guid.Empty;
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("properties")]
        public IReadOnlyList<Property> Properties { get; init; } = [];
        public ResolvedProfile()
        {
            
        }
        public ResolvedProfile(Guid id, string name)
        {
            Guid = id;
            Name = name;
        }
        public ResolvedProfile(Guid id, string name, IReadOnlyList<Property> properties)
        {
            Guid = id;
            Name = name;
            Properties = properties;
        }
        
        internal ResolvedProfile(Stream stream)
        {
            Guid = stream.ReadGuid();
            Name = stream.ReadT16AS32V();
            Property[] properties = new Property[stream.ReadS32V()];
            for (int i = 0; i < properties.Length; i++)
            {
                properties[i] = new(stream);
            }
            Properties = properties;
        }
        
        internal void Serialize(Stream stream)
        {
            stream.WriteGuid(Guid);
            stream.WriteT16AS32V(Name);
            stream.WriteS32V(Properties.Count);
            foreach(Property property in Properties)
            {
                property.Serialize(stream);
            }
        }
    }
}
