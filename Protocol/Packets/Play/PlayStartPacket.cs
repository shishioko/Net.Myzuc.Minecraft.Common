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

        public int EntityId { get; set; } = 0;
        public bool Hardcore { get; set; } = false;
        public IList<Identifier> DimensionNames = [];
        public int TablistSize { get; set; } = 0;
        public int ViewDistance { get; set; } = 0;
        public int SimulationDistance { get; set; } = 0;
        public bool ReduceDebugInfo { get; set; } = false;
        public bool ShowRespawnScreen { get; set; } = false;
        public bool LimitedCrafting { get; set; } = false;
        public RespawnMetadata RespawnMetadata { get; set; } = new();
        public bool EnableCensorship { get; set; } = false;
        
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
            RespawnMetadata = new(stream);
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
            RespawnMetadata.Serialize(stream);
            stream.WriteBool(EnableCensorship);
        }
    }
}