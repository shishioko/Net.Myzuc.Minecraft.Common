using Net.Myzuc.Minecraft.Common.Data.Primitives;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public readonly record struct GlobalLocation
    {
        public Identifier Dimension { get; } = new();
        public Location Location { get; } = new();
        public GlobalLocation()
        {
            
        }
        public GlobalLocation(Identifier dimension)
        {
            Dimension = dimension;
        }
        public GlobalLocation(Identifier dimension, Location location)
        {
            Dimension = dimension;
            Location = location;
        }
        
        internal GlobalLocation(Stream stream)
        {
            Dimension = new(stream);
            Location = new(stream);
        }
        
        internal void Serialize(Stream stream)
        {
            Dimension.Serialize(stream);
            Location.Serialize(stream);
        }
    }
}