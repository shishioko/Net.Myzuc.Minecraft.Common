namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public record class NbtSerializerOptions
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
        public bool PolymorphicSerialization = true;
        public bool IgnoreReadOnlyFields = false;
        public bool IgnoreReadOnlyProperties = false;
        public bool IncludeFields = false;
        public int MaxDepth = 512;
        public NbtUnmappedMemberHandling UnmappedMemberHandling = NbtUnmappedMemberHandling.Skip;
        public NbtSerializerOptions()
        {
            
        }
        public NbtSerializerOptions(NbtSerializerOptions original)
        {
            PolymorphicSerialization = original.PolymorphicSerialization;
            Converters = new List<NbtConverter>(original.Converters);
            IgnoreReadOnlyFields = original.IgnoreReadOnlyFields;
            IgnoreReadOnlyProperties = original.IgnoreReadOnlyProperties;
            IncludeFields = original.IncludeFields;
            MaxDepth = original.MaxDepth;
            UnmappedMemberHandling = original.UnmappedMemberHandling;
        }
    }
}