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
        
        internal static void Serialize(Stream stream, ResolvedProfile resolvedProfile)
        {
            stream.WriteGuid(resolvedProfile.Guid);
            stream.WriteT16AS32V(resolvedProfile.Name);
            stream.WriteS32V(resolvedProfile.Properties.Count);
            foreach(Property property in resolvedProfile.Properties)
            {
                stream.WriteT16AS32V(property.Name);
                stream.WriteT16AS32V(property.Value);
                stream.WriteBool(property.Signature is not null);
                if (property.Signature is not null)
                {
                    stream.WriteT16AS32V(property.Signature);
                }
            }
        }
        internal static ResolvedProfile Deserialize(Stream stream)
        {
            ResolvedProfile profile = new();
            profile.Guid = stream.ReadGuid();
            profile.Name = stream.ReadT16AS32V();
            profile.Properties = new Property[stream.ReadS32V()];
            for (int i = 0; i < profile.Properties.Count; i++)
            {
                Property property = new();
                property.Name = stream.ReadT16AS32V();
                property.Value = stream.ReadT16AS32V();
                if (stream.ReadBool())
                {
                    property.Signature = stream.ReadT16AS32V();
                }
                profile.Properties[i] = property;
            }
            return profile;
        }
    }
}
