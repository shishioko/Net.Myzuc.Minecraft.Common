using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public sealed record GlobalLocation : IBinarySerializable<GlobalLocation>
    {
        public Identifier Dimension { get; set; } = new();
        public Location Location { get; set; } = new();
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
        
        static GlobalLocation IBinarySerializable<GlobalLocation>.Deserialize(Stream stream)
        {
            GlobalLocation data = new();
            data.Dimension = stream.Read<Identifier>();
            data.Location = stream.Read<Location>();
            return data;
        }
        static void IBinarySerializable<GlobalLocation>.Serialize(GlobalLocation data, Stream stream)
        {
            stream.Write(data.Dimension);
            stream.Write(data.Location);
        }
    }
}