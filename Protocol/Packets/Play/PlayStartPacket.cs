using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Data.Structs;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Play
{
    public sealed record PlayStartPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Play;
        protected internal override int PacketId => 0x30;

        public int EntityId { get; init; } = 0;
        public bool Hardcore { get; init; } = false;
        public IReadOnlyList<Identifier> DimensionNames = [];
        public int TablistSize { get; init; } = 0;
        public int ViewDistance { get; init; } = 0;
        public int SimulationDistance { get; init; } = 0;
        public bool ReduceDebugInfo { get; init; } = false;
        public bool ShowRespawnScreen { get; init; } = false;
        public bool LimitedCrafting { get; init; } = false;
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
        public bool EnableCensorship { get; init; } = false;
        
        public PlayStartPacket()
        {
            
        }

        internal PlayStartPacket(Stream stream) : base(stream)
        {
            EntityId = stream.ReadS32();
            Hardcore = stream.ReadBool();
            Identifier[] dimensionNames = new Identifier[stream.ReadS32V()];
            for (int i = 0; i < dimensionNames.Length; i++)
            {
                dimensionNames[i] = new(stream);
            }
            DimensionNames = dimensionNames;
            TablistSize = stream.ReadS32V();
            ViewDistance = stream.ReadS32V();
            SimulationDistance = stream.ReadS32V();
            ReduceDebugInfo = stream.ReadBool();
            ShowRespawnScreen = stream.ReadBool();
            LimitedCrafting = stream.ReadBool();
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
            EnableCensorship = stream.ReadBool();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32(EntityId);
            stream.WriteBool(Hardcore);
            stream.WriteS32V(DimensionNames.Count);
            foreach (Identifier dimensionName in DimensionNames)
            {
                dimensionName.Serialize(stream);
            }
            stream.WriteS32V(TablistSize);
            stream.WriteS32V(ViewDistance);
            stream.WriteS32V(SimulationDistance);
            stream.WriteBool(ReduceDebugInfo);
            stream.WriteBool(ShowRespawnScreen);
            stream.WriteBool(LimitedCrafting);
            stream.WriteS32V(CurrentDimensionTypeId); //
            CurrentDimensionName.Serialize(stream); //
            stream.WriteS64(NoiseSeed); //
            stream.WriteS8((sbyte)CurrentGamemode); //
            stream.WriteS8((sbyte)PreviousGamemode); //
            stream.WriteBool(IsDebugWorld); //
            stream.WriteBool(IsSuperflatWorld); //
            stream.WriteBool(DeathLocation.HasValue); //
            if (DeathLocation.HasValue) //
            {
                DeathLocation.Value.Serialize(stream); //
            }
            stream.WriteS32V(PortalCooldown); //
            stream.WriteS32V(OceanHeight); //
            stream.WriteBool(EnableCensorship);
        }
    }
}