namespace Net.Myzuc.Minecraft.Common.Nbt
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple=false)]
    public sealed class NbtUnmappedMemberHandlingAttribute : NbtAttribute
    {
        public NbtUnmappedMemberHandling UnmappedMemberHandling { get; }
        public NbtUnmappedMemberHandlingAttribute(NbtUnmappedMemberHandling unmappedMemberHandling)
        {
            UnmappedMemberHandling = unmappedMemberHandling;
        }
    }
}