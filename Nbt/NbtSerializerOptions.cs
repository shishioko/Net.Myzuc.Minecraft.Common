namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public sealed class NbtSerializerOptions
    {
        public static NbtSerializerOptions Default { get; } = new()
        {
            UnmappedMemberHandling = NbtUnmappedMemberHandling.Skip,
        };
        public static NbtSerializerOptions Strict { get; } = new()
        {
            UnmappedMemberHandling = NbtUnmappedMemberHandling.Disallow,
        };
        public IList<NbtConverter> Converters = new List<NbtConverter>().AsReadOnly();
        public bool IgnoreReadOnlyFields = false;
        public bool IgnoreReadOnlyProperties = false;
        public bool IncludeFields = false;
        public int MaxDepth = 512;
        public NbtUnmappedMemberHandling UnmappedMemberHandling = NbtUnmappedMemberHandling.Skip;
    }
}