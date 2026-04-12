using Net.Myzuc.Minecraft.Common.Enums;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Serverbound
{
    public sealed record ClientSettingsPacket : IPacket
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

        public ClientSettingsPacket()
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
        void IPacket.Deserialize(Stream stream)
        {
            ClientSettingsPacket packet = new();
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
    }
}