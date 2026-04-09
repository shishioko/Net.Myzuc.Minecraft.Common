using System.Runtime.Serialization;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public abstract class NbtConverter
    {
        public abstract bool CanConvert(Type type);
        public abstract NbtTag? Serialize(object? data, NbtSerializerOptions options, int depth);
        public abstract object? Deserialize(NbtTag nbt, NbtSerializerOptions options, int depth);
    }
    public abstract class NbtConverter<T> : NbtConverter
    {
        public sealed override bool CanConvert(Type type)
        {
            return typeof(T).IsAssignableTo(type);
        }
        public sealed override NbtTag? Serialize(object? data, NbtSerializerOptions options, int depth)
        {
            if (data is not T value) throw new SerializationException();
            return SerializeValue(value, options, depth);
        }
        public sealed override object? Deserialize(NbtTag nbt, NbtSerializerOptions options, int depth)
        {
            return DeserializeValue(nbt, options, depth);
        }
        public abstract NbtTag? SerializeValue(T data, NbtSerializerOptions options, int depth);
        public abstract T DeserializeValue(NbtTag nbt, NbtSerializerOptions options, int depth);
    }
}