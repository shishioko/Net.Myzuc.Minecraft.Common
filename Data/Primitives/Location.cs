using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Data.Primitives
{
    public readonly record struct Location
    {
        public long Value { get; }
        [IgnoreDataMember]
        public int X
        {
            get
            {
                int value = (int)((ulong)Value >> 38);
                if ((value & 0x02000000) != 0) value -= 0x04000000;
                return value;
            }
        }
        [IgnoreDataMember]
        public int Z
        {
            get
            {
                int value = (int)(((ulong)Value >> 12) & 0x3FFFFFF);
                if ((value & 0x02000000) != 0) value -= 0x4000000;
                return value;
            }
        }
        [IgnoreDataMember]
        public int Y
        {
            get
            {
                int value = (int)(((ulong)Value & 0xFFF));
                if ((value & 0x00000800) != 0) value -= 0x1000;
                return value;
            }
        }
        
        public Location()
        {
            Value = 0;
        }
        public Location(long value)
        {
            Value = value;
        }
        public Location(int x, int y, int z)
        {
            Value = (long)((ulong)y | (ulong)(z << 12) | ((ulong)x << 38));
        }
        
        internal Location(Stream stream)
        {
            Value = stream.ReadS64();
        }
        
        internal void Serialize(Stream stream)
        {
            stream.WriteS64(Value);
        }
        
        public static Location operator +(Location a, Location b)
        {
            return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Location operator -(Location a, Location b)
        {
            return new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
    }
}