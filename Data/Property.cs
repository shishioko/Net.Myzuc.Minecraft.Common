using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly record struct Property
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = "";
        [JsonPropertyName("value")]
        public string Value { get; init; } = "";
        [JsonPropertyName("signature")]
        public string? Signature { get; } = null;
        public Property()
        {
            
        }
        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public Property(string name, string value, string? signature)
        {
            Name = name;
            Value = value;
            Signature = signature;
        }
        
        internal Property(Stream stream)
        {
            Name = stream.ReadT16AS32V();
            Value = stream.ReadT16AS32V();
            if (stream.ReadBool())
            {
                Signature = stream.ReadT16AS32V();
            }
        }
        
        internal void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Name);
            stream.WriteT16AS32V(Value);
            stream.WriteBool(Signature is not null);
            if (Signature is not null)
            {
                stream.WriteT16AS32V(Signature);
            }
        }
    }
}