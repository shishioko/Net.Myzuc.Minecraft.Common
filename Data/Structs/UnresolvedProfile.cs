using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public sealed record UnresolvedProfile : INbtSerializable<UnresolvedProfile>
    {
        [JsonConverter(typeof(GuidNbtJsonConverter))]
        [JsonPropertyName("id")]
        public Guid? Guid { get; set; } = null;
        [JsonPropertyName("name")]
        public string? Name { get; set; } = null;
        [JsonPropertyName("properties")]
        public IReadOnlyList<Property>? Properties { get; set; } = null;
        [JsonPropertyName("texture")]
        public string? Skin { get; set; } = null;
        [JsonPropertyName("cape")]
        public string? Cape { get; set; } = null;
        [JsonPropertyName("elytra")]
        public string? Elytra { get; set; } = null;
        [JsonPropertyName("model")]
        public PlayerModelType? ModelType { get; set; } = null;
        public UnresolvedProfile()
        {
            
        }
        public UnresolvedProfile(Guid guid)
        {
            Guid = guid;
        }
        public UnresolvedProfile(string name)
        {
            Name = name;
        }
        public UnresolvedProfile(ResolvedProfile profile)
        {
            Guid = profile.Guid;
            Name = profile.Name;
            Properties = profile.Properties;
        }
        
        static UnresolvedProfile INbtSerializable<UnresolvedProfile>.FromNbt(NbtTag nbt)
        {
            if (nbt is not CompoundNbtTag compound) throw new SerializationException();
            UnresolvedProfile data = new();
            if (compound.ContainsKey("id")) data.Guid = compound["id"].Get<IntArrayNbtTag>();
            if (compound.ContainsKey("name")) data.Name = compound["name"].Get<StringNbtTag>();
            if (compound.ContainsKey("properties"))
            {
                data.Properties = compound["properties"].Get<ListNbtTag>().Select(Nbt.Nbt.FromNbt<Property>).ToList();
            }
            if (compound.ContainsKey("texture")) data.Skin = compound["texture"].Get<StringNbtTag>();
            if (compound.ContainsKey("cape")) data.Cape = compound["cape"].Get<StringNbtTag>();
            if (compound.ContainsKey("elytra")) data.Elytra = compound["elytra"].Get<StringNbtTag>();
            if (compound.ContainsKey("model")) data.ModelType = (PlayerModelType)compound["model"].Get<IntNbtTag>().Value;
            return data;
        }
        static NbtTag INbtSerializable<UnresolvedProfile>.ToNbt(UnresolvedProfile data)
        {
            CompoundNbtTag nbt = new();
            if (data.Guid is not null) nbt["id"] = (IntArrayNbtTag)data.Guid;
            if (data.Name is not null) nbt["name"] = (StringNbtTag)data.Name;
            if (data.Properties is not null)
            {
                nbt["properties"] = new ListNbtTag(data.Properties.Select(Nbt.Nbt.ToNbt).ToList());
            }
            if (data.Skin is not null) nbt["skin"] = (StringNbtTag)data.Skin;
            if (data.Cape is not null) nbt["cape"] = (StringNbtTag)data.Cape;
            if (data.Elytra is not null) nbt["elytra"] = (StringNbtTag)data.Elytra;
            if (data.ModelType.HasValue) nbt["model"] = (IntNbtTag)(int)data.ModelType;
            return nbt;
        }
    }
}