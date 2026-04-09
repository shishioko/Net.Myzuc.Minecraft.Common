namespace Net.Myzuc.Minecraft.Common.Nbt
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public sealed class NbtPropertyAttribute : NbtAttribute
    {
        public string Name { get; }
        public NbtPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}