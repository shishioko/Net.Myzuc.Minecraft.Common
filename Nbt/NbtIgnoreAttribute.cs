namespace Net.Myzuc.Minecraft.Common.Nbt
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NbtIgnoreAttribute : NbtAttribute
    {
        public NbtIgnoreAttribute()
        {
            
        }
    }
}