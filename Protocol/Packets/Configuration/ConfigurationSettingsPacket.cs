using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationSettingsPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x00;

        public string Locale { get; init; } = string.Empty;
        public byte ViewDistance { get; init; } = 0;
        public ChatMode ChatMode { get; init; } = ChatMode.Enabled;
        public bool ChatColors { get; init; } = false;
        public SkinPartFlags SkinParts { get; init; } = SkinPartFlags.None;
        public MainHand MainHand { get; init; } = MainHand.Left;
        public bool EnableCensorship { get; init; } = false;
        public bool AllowListing { get; init; } = false;
        public ParticleSetting ParticleSettings { get; init; } = ParticleSetting.All;

        public ConfigurationSettingsPacket()
        {
            
        }

        internal ConfigurationSettingsPacket(Stream stream) : base(stream)
        {
            Locale = stream.ReadT16AS32V();
            ViewDistance = stream.ReadU8();
            ChatMode = (ChatMode)stream.ReadS32V();
            ChatColors = stream.ReadBool();
            SkinParts = (SkinPartFlags)stream.ReadU8();
            MainHand = (MainHand)stream.ReadS32V();
            EnableCensorship = stream.ReadBool();
            AllowListing = stream.ReadBool();
            ParticleSettings = (ParticleSetting)stream.ReadS32V();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Locale);
            stream.WriteU8(ViewDistance);
            stream.WriteS32V((int)ChatMode);
            stream.WriteBool(ChatColors);
            stream.WriteU8((byte)SkinParts);
            stream.WriteS32V((int)MainHand);
            stream.WriteBool(EnableCensorship);
            stream.WriteBool(AllowListing);
            stream.WriteS32V((int)ParticleSettings);
        }
    }
}