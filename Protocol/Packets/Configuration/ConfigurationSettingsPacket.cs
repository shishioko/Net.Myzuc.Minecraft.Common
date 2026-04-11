using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationSettingsPacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x00;

        public string Locale { get; set; } = string.Empty;
        public byte ViewDistance { get; set; } = 0;
        public ChatMode ChatMode { get; set; } = ChatMode.Enabled;
        public bool ChatColors { get; set; } = false;
        public SkinPartFlags SkinParts { get; set; } = SkinPartFlags.None;
        public MainHand MainHand { get; set; } = MainHand.Left;
        public bool EnableCensorship { get; set; } = false;
        public bool AllowListing { get; set; } = false;
        public ParticleSetting ParticleSettings { get; set; } = ParticleSetting.All;

        public ConfigurationSettingsPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            ConfigurationSettingsPacket packet = new();
            packet.Locale = stream.ReadT16AS32V();
            packet.ViewDistance = stream.ReadU8();
            packet.ChatMode = (ChatMode)stream.ReadS32V();
            packet.ChatColors = stream.ReadBool();
            packet.SkinParts = (SkinPartFlags)stream.ReadU8();
            packet.MainHand = (MainHand)stream.ReadS32V();
            packet.EnableCensorship = stream.ReadBool();
            packet.AllowListing = stream.ReadBool();
            packet.ParticleSettings = (ParticleSetting)stream.ReadS32V();
            return packet;
        }
    }
}