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
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            UnresolvedProfile data = new();
            if (compoundNbt.ContainsKey("id")) data.Guid = compoundNbt["id"].Get<IntArrayNbtTag>();
            if (compoundNbt.ContainsKey("name")) data.Name = compoundNbt["name"].Get<StringNbtTag>();
            if (compoundNbt.ContainsKey("properties"))
            {
                data.Properties = compoundNbt["properties"].Get<ListNbtTag>().Select(Nbt.Nbt.FromNbt<Property>).ToList();
            }
            if (compoundNbt.ContainsKey("texture")) data.Skin = compoundNbt["texture"].Get<StringNbtTag>();
            if (compoundNbt.ContainsKey("cape")) data.Cape = compoundNbt["cape"].Get<StringNbtTag>();
            if (compoundNbt.ContainsKey("elytra")) data.Elytra = compoundNbt["elytra"].Get<StringNbtTag>();
            if (compoundNbt.ContainsKey("model")) data.ModelType = (PlayerModelType)compoundNbt["model"].Get<IntNbtTag>().Value;
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