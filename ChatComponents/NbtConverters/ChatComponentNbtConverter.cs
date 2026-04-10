using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents.NbtConverters
{
    internal sealed class ChatComponentNbtConverter : NbtConverter<ChatComponent>
    {
        public override NbtTag? SerializeValue(ChatComponent? data, NbtSerializerContext context)
        {
            if (data is null) return null;
            return NbtSerializer.Serialize(data, context);
        }
        public override ChatComponent? DeserializeValue(NbtTag? nbt, NbtSerializerContext context)
        {
            if (nbt is null) return null;
            if (nbt is StringNbtTag stringNbt) return new TextChatComponent(stringNbt.Value);
            if (nbt is ListNbtTag listNbt)
                return new TextChatComponent()
                {
                    Children = listNbt.Select(child => NbtSerializer.Deserialize<ChatComponent>(child, context)).Where(child => child is not null)!,
                };
            ChatComponent? result = nbt.As<CompoundNbtTag>() switch
            {
                var compound when compound.TryGetValue("type", out NbtTag? type) => type.As<StringNbtTag>().Value switch
                {
                    "text" => NbtSerializer.Deserialize<TextChatComponent>(nbt, context),
                    "translate" => NbtSerializer.Deserialize<TranslatableChatComponent>(nbt, context),
                    "keybind" => NbtSerializer.Deserialize<KeybindChatComponent>(nbt, context),
                    "object" => NbtSerializer.Deserialize<ObjectChatComponent>(nbt, context),
                    _ => throw new SerializationException()
                },
                var compound when compound.TryGetValue("text", out _) => NbtSerializer.Deserialize<TextChatComponent>(nbt, context),
                var compound when compound.TryGetValue("translate", out _) => NbtSerializer.Deserialize<TranslatableChatComponent>(nbt, context),
                var compound when compound.TryGetValue("keybind", out _) => NbtSerializer.Deserialize<KeybindChatComponent>(nbt, context),
                var compound when compound.TryGetValue("object", out _) => NbtSerializer.Deserialize<ObjectChatComponent>(nbt, context),
                _ => NbtSerializer.Deserialize<TextChatComponent>(nbt, context),
            };
            return result ?? throw new SerializationException();
        }
    }
}