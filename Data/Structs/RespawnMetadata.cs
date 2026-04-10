using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public record struct RespawnMetadata
    {
        public int CurrentDimensionTypeId { get; init; } = 0;
        public Identifier CurrentDimensionName { get; init; } = new();
        public long NoiseSeed { get; init; } = 0;
        public Gamemode CurrentGamemode { get; init; } = Gamemode.Survival;
        public Gamemode PreviousGamemode { get; init; } = Gamemode.Survival;
        public bool IsDebugWorld { get; init; } = false;
        public bool IsSuperflatWorld { get; init; } = false;
        public GlobalLocation? DeathLocation { get; init; } = null;
        public int PortalCooldown { get; init; } = 0;
        public int OceanHeight { get; init; } = 0;
        
        public RespawnMetadata()
        {
            
        }

        internal RespawnMetadata(Stream stream)
        {
            CurrentDimensionTypeId = stream.ReadS32V();
            CurrentDimensionName = new(stream);
            NoiseSeed = stream.ReadS64();
            CurrentGamemode = (Gamemode)stream.ReadS8();
            PreviousGamemode = (Gamemode)stream.ReadS8();
            IsDebugWorld = stream.ReadBool();
            IsSuperflatWorld = stream.ReadBool();
            if (stream.ReadBool())
            {
                DeathLocation = new(stream);
            }
            PortalCooldown = stream.ReadS32V();
            OceanHeight = stream.ReadS32V();
        }
        
        internal void Serialize(Stream stream)
        {
            stream.WriteS32V(CurrentDimensionTypeId);
            CurrentDimensionName.Serialize(stream);
            stream.WriteS64(NoiseSeed);
            stream.WriteS8((sbyte)CurrentGamemode);
            stream.WriteS8((sbyte)PreviousGamemode);
            stream.WriteBool(IsDebugWorld);
            stream.WriteBool(IsSuperflatWorld);
            stream.WriteBool(DeathLocation.HasValue);
            if (DeathLocation.HasValue)
            {
                DeathLocation.Value.Serialize(stream);
            }
            stream.WriteS32V(PortalCooldown);
            stream.WriteS32V(OceanHeight);
        }
    }
}