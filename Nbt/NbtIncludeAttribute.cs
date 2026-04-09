namespace Net.Myzuc.Minecraft.Common.Nbt
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NbtIncludeAttribute : NbtAttribute
    {
        public NbtIncludeAttribute()
        {
            
        }
    }
}