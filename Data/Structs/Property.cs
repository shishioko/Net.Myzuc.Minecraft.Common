using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public sealed record Property : IBinarySerializable<Property>, INbtSerializable<Property>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("value")]
        public string Value { get; set; } = "";
        [JsonPropertyName("signature")]
        public string? Signature { get; set; } = null;
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
        
        static Property IBinarySerializable<Property>.Deserialize(Stream stream)
        {
            Property data = new();
            data.Name = stream.ReadT16AS32V();
            data.Value = stream.ReadT16AS32V();
            if (stream.ReadBool()) data.Signature = stream.ReadT16AS32V();
            return data;
        }
        static void IBinarySerializable<Property>.Serialize(Property data, Stream stream)
        {
            stream.WriteT16AS32V(data.Name);
            stream.WriteT16AS32V(data.Value);
            stream.WriteBool(data.Signature is not null);
            if (data.Signature is not null)
            {
                stream.WriteT16AS32V(data.Signature);
            }
        }
        static Property INbtSerializable<Property>.FromNbt(NbtTag nbt)
        {
            if (nbt is not CompoundNbtTag compoundNbt) throw new SerializationException();
            Property data = new();
            data.Name = compoundNbt["name"].Get<StringNbtTag>();
            data.Value = compoundNbt["value"].Get<StringNbtTag>();
            if (compoundNbt.ContainsKey("signature"))
            {
                data.Signature = compoundNbt["signature"].Get<StringNbtTag>();
            }
            return data;
        }
        static NbtTag INbtSerializable<Property>.ToNbt(Property data)
        {
            CompoundNbtTag nbt = new();
            nbt["name"] = (StringNbtTag)data.Name;
            nbt["value"] = (StringNbtTag)data.Value;
            if (data.Signature is not null) nbt["signature"] = (StringNbtTag)data.Signature;
            return nbt;
        }
    }
}