namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public sealed class NbtSerializerOptions
    {
        public IList<NbtConverter> Converters = new List<NbtConverter>().AsReadOnly();
        public bool IgnoreReadOnlyFields = false;
        public bool IgnoreReadOnlyProperties = false;
        public bool IncludeFields = false;
        public int MaxDepth = 512;
        public NbtUnmappedMemberHandling UnmappedMemberHandling = NbtUnmappedMemberHandling.Skip;
    }
}