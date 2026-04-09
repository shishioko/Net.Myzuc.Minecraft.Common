namespace Net.Myzuc.Minecraft.Common.Nbt
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple=false)]
    public sealed class NbtConverterAttribute : NbtAttribute
    {
        public Type Type { get; }
        public NbtConverterAttribute(Type type)
        {
            Type = type;
        }
    }
}