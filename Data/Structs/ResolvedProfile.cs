using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public sealed record ResolvedProfile : IBinarySerializable<ResolvedProfile>
    {
        [JsonConverter(typeof(GuidStringJsonConverter))]
        [JsonPropertyName("id")]
        public Guid Guid { get; set; } = Guid.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("properties")]
        public IReadOnlyList<Property> Properties { get; set; } = [];
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
        
        static ResolvedProfile IBinarySerializable<ResolvedProfile>.Deserialize(Stream stream)
        {
            ResolvedProfile data = new();
            data.Guid = stream.ReadGuid();
            data.Name = stream.ReadT16AS32V();
            Property[] properties = new Property[stream.ReadS32V()];
            for (int i = 0; i < properties.Length; i++)
            {
                properties[i] = stream.Read<Property>();
            }
            data.Properties = properties;
            return data;
        }
        static void IBinarySerializable<ResolvedProfile>.Serialize(ResolvedProfile data, Stream stream)
        {
            stream.WriteGuid(data.Guid);
            stream.WriteT16AS32V(data.Name);
            stream.WriteS32V(data.Properties.Count);
            foreach(Property property in data.Properties)
            {
                stream.Write(property);
            }
        }
    }
}
