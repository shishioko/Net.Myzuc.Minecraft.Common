using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents.NbtConverters
{
    internal sealed class ObjectChatComponentNbtConverter : NbtConverter<ObjectChatComponent>
    {
        public override NbtTag? SerializeValue(ObjectChatComponent? data, NbtSerializerContext context)
        {
            if (data is null) return null;
            return NbtSerializer.Serialize(data, context);
        }
        public override ObjectChatComponent? DeserializeValue(NbtTag? nbt, NbtSerializerContext context)
        {
            if (nbt is null) return null;
            ObjectChatComponent? result = nbt.As<CompoundNbtTag>() switch
            {
                var compound when compound.TryGetValue("object", out NbtTag? type) => type.As<StringNbtTag>().Value switch
                {
                    "atlas" => NbtSerializer.Deserialize<AtlasObjectChatComponent>(nbt, context),
                    "player" => NbtSerializer.Deserialize<PlayerObjectChatComponent>(nbt, context),
                    _ => throw new SerializationException()
                },
                var compound when compound.TryGetValue("atlas", out _) => NbtSerializer.Deserialize<AtlasObjectChatComponent>(nbt, context),
                var compound when compound.TryGetValue("sprite", out _) => NbtSerializer.Deserialize<AtlasObjectChatComponent>(nbt, context),
                var compound when compound.TryGetValue("player", out _) => NbtSerializer.Deserialize<PlayerObjectChatComponent>(nbt, context),
                _ => NbtSerializer.Deserialize<AtlasObjectChatComponent>(nbt, context),
            };
            return result ?? throw new SerializationException();
        }
    }
}