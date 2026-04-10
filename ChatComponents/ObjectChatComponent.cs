using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ObjectChatComponentJsonConverter))]
    public abstract record ObjectChatComponent : ChatComponent
    {
        protected override string Type => "object";
        [JsonInclude]
        [JsonPropertyName("object")]
        protected abstract string Object { get; }
        protected internal ObjectChatComponent()
        {
            
        }
        
        internal ObjectChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            
        }

        internal override CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = base.Serialize();
            nbt["object"] = (StringNbtTag)Object;
            return nbt;
        }

        internal static ObjectChatComponent Deserialize(CompoundNbtTag nbt)
        {
            return nbt switch
            {
                _ when nbt.ContainsKey("object") => nbt["object"].As<StringNbtTag>().Value switch
                {
                    "atlas" => new AtlasObjectChatComponent(nbt),
                    "player" => new PlayerObjectChatComponent(nbt),
                    _ => throw new SerializationException()
                },
                _ when nbt.ContainsKey("atlas") => new AtlasObjectChatComponent(nbt),
                _ when nbt.ContainsKey("sprite") => new AtlasObjectChatComponent(nbt),
                _ when nbt.ContainsKey("player") => new PlayerObjectChatComponent(nbt),
                _ => new AtlasObjectChatComponent(nbt),
            };
        }
    }
}