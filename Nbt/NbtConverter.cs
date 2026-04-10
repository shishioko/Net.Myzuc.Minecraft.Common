using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public abstract class NbtConverter
    {
        public abstract bool CanConvert(Type type);
        public abstract NbtTag? Serialize(object? data, NbtSerializerContext context);
        public abstract object? Deserialize(NbtTag? nbt, NbtSerializerContext context);
    }
    public abstract class NbtConverter<T> : NbtConverter
    {
        public sealed override bool CanConvert(Type type)
        {
            return type.IsAssignableTo(typeof(T));
        }
        public sealed override NbtTag? Serialize(object? data, NbtSerializerContext context)
        {
            return SerializeValue(data is T value ? value : default, context);
        }
        public sealed override object? Deserialize(NbtTag? nbt, NbtSerializerContext context)
        {
            return DeserializeValue(nbt, context);
        }
        public abstract NbtTag? SerializeValue(T? data, NbtSerializerContext context);
        public abstract T? DeserializeValue(NbtTag? nbt, NbtSerializerContext context);
    }
}