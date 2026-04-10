namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public sealed record NbtSerializerContext
    {
        public NbtSerializerOptions Options = NbtSerializerOptions.Default;
        public bool IgnoreConverter = false;
        public int Depth = 0;
        public NbtSerializerContext()
        {
            
        }
        public NbtSerializerContext(NbtSerializerOptions? options = null)
        {
            Options = options ?? NbtSerializerOptions.Default;
        }
        private NbtSerializerContext(NbtSerializerContext original)
        {
            Options = original.Options;
        }
        internal NbtSerializerContext Derive(bool ignoreConverter)
        {
            return new(this)
            {
                IgnoreConverter = ignoreConverter,
                Depth = Depth + 1,
            };
        }
    }
}