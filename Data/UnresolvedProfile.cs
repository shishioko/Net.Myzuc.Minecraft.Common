using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly record struct UnresolvedProfile
    {
        [JsonConverter(typeof(GuidNbtJsonConverter))]
        [JsonPropertyName("id")]
        public Guid? Guid { get; init; } = null;
        [JsonPropertyName("name")]
        public string? Name { get; init; } = null;
        [JsonPropertyName("properties")]
        public IReadOnlyList<Property>? Properties { get; init; } = null;
        [JsonPropertyName("texture")]
        public string? Skin { get; init; } = null;
        [JsonPropertyName("cape")]
        public string? Cape { get; init; } = null;
        [JsonPropertyName("elytra")]
        public string? Elytra { get; init; } = null;
        [JsonPropertyName("model")]
        public PlayerModelType? ModelType { get; init; } = null;
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
        
        internal UnresolvedProfile(CompoundNbtTag nbt)
        {
            if (nbt.ContainsKey("id")) Guid = nbt["id"].Get<IntArrayNbtTag>();
            if (nbt.ContainsKey("name")) Name = nbt["name"].Get<StringNbtTag>();
            if (nbt.ContainsKey("properties"))
            {
                Properties = nbt["properties"].Get<ListNbtTag>().Select(property => new Property(property.Get<CompoundNbtTag>())).ToList();
            }
            if (nbt.ContainsKey("texture")) Skin = nbt["texture"].Get<StringNbtTag>();
            if (nbt.ContainsKey("cape")) Cape = nbt["cape"].Get<StringNbtTag>();
            if (nbt.ContainsKey("elytra")) Elytra = nbt["elytra"].Get<StringNbtTag>();
            if (nbt.ContainsKey("model")) ModelType = (PlayerModelType)nbt["model"].Get<IntNbtTag>().Value;
        }

        internal CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = new();
            if (Guid is not null) nbt["id"] = (IntArrayNbtTag)Guid;
            if (Name is not null) nbt["name"] = (StringNbtTag)Name;
            if (Properties is not null)
            {
                nbt["properties"] = new ListNbtTag(Properties.Select(entry => entry.Serialize()).ToList());
            }
            if (Skin is not null) nbt["skin"] = (StringNbtTag)Skin;
            if (Cape is not null) nbt["cape"] = (StringNbtTag)Cape;
            if (Elytra is not null) nbt["elytra"] = (StringNbtTag)Elytra;
            if (ModelType.HasValue) nbt["model"] = (IntNbtTag)(int)ModelType;
            return nbt;
        }
    }
}