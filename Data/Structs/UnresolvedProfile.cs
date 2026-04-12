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
        NbtTag INbtSerializable<UnresolvedProfile>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            if (Guid is not null) nbt["id"] = (IntArrayNbtTag)Guid;
            if (Name is not null) nbt["name"] = (StringNbtTag)Name;
            if (Properties is not null)
            {
                nbt["properties"] = new ListNbtTag(Properties.Select(Nbt.Nbt.ToNbt).ToList());
            }
            if (Skin is not null) nbt["skin"] = (StringNbtTag)Skin;
            if (Cape is not null) nbt["cape"] = (StringNbtTag)Cape;
            if (Elytra is not null) nbt["elytra"] = (StringNbtTag)Elytra;
            if (ModelType.HasValue) nbt["model"] = (IntNbtTag)(int)ModelType;
            return nbt;
        }
    }
}