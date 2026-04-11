using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public sealed record RespawnMetadata : IBinarySerializable<RespawnMetadata>
    {
        public int CurrentDimensionTypeId { get; set; } = 0;
        public Identifier CurrentDimensionName { get; set; } = new();
        public long NoiseSeed { get; set; } = 0;
        public Gamemode CurrentGamemode { get; set; } = Gamemode.Survival;
        public Gamemode PreviousGamemode { get; set; } = Gamemode.Survival;
        public bool IsDebugWorld { get; set; } = false;
        public bool IsSuperflatWorld { get; set; } = false;
        public GlobalLocation? DeathLocation { get; set; } = null;
        public int PortalCooldown { get; set; } = 0;
        public int OceanHeight { get; set; } = 0;
        
        public RespawnMetadata()
        {
            
        }
        
        static RespawnMetadata IBinarySerializable<RespawnMetadata>.Deserialize(Stream stream)
        {
            RespawnMetadata data = new();
            data.CurrentDimensionTypeId = stream.ReadS32V();
            data.CurrentDimensionName = stream.Read<Identifier>();
            data.NoiseSeed = stream.ReadS64();
            data.CurrentGamemode = (Gamemode)stream.ReadS8();
            data.PreviousGamemode = (Gamemode)stream.ReadS8();
            data.IsDebugWorld = stream.ReadBool();
            data.IsSuperflatWorld = stream.ReadBool();
            if (stream.ReadBool())
            {
                data.DeathLocation = stream.Read<GlobalLocation>();
            }
            data.PortalCooldown = stream.ReadS32V();
            data.OceanHeight = stream.ReadS32V();
            return data;
        }
        static void IBinarySerializable<RespawnMetadata>.Serialize(RespawnMetadata data, Stream stream)
        {
            stream.WriteS32V(data.CurrentDimensionTypeId);
            stream.Write(data.CurrentDimensionName);
            stream.WriteS64(data.NoiseSeed);
            stream.WriteS8((sbyte)data.CurrentGamemode);
            stream.WriteS8((sbyte)data.PreviousGamemode);
            stream.WriteBool(data.IsDebugWorld);
            stream.WriteBool(data.IsSuperflatWorld);
            stream.WriteBool(data.DeathLocation is not null);
            if (data.DeathLocation is not null)
            {
                stream.Write(data.DeathLocation);
            }
            stream.WriteS32V(data.PortalCooldown);
            stream.WriteS32V(data.OceanHeight);
        }
    }
}