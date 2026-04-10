using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
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

        internal Property(CompoundNbtTag nbt)
        {
            Name = nbt["name"].Get<StringNbtTag>();
            Value = nbt["value"].Get<StringNbtTag>();
            if (nbt.ContainsKey("signature")) Signature = nbt["value"].Get<StringNbtTag>();
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
        
        internal CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = new();
            nbt["name"] = (StringNbtTag)Name;
            nbt["value"] = (StringNbtTag)Value;
            if (Signature is not null) nbt["signature"] = (StringNbtTag)Signature;
            return nbt;
        }
    }
}