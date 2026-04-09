using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public class ConfigurationSettingsPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x00;

        public string Locale = string.Empty;
        public byte ViewDistance = 0;
        public ChatMode ChatMode = ChatMode.Enabled;
        public bool ChatColors = false;
        public SkinPartFlags SkinParts = SkinPartFlags.None;
        public MainHand MainHand = MainHand.Left;
        public bool EnableCensorship = false;
        public bool AllowListing = false;
        public ParticleSetting ParticleSettings = ParticleSetting.All;

        internal override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(Locale);
            stream.WriteU8(ViewDistance);
            stream.WriteS32V((int)ChatMode);
            stream.WriteBool(ChatColors);
            stream.WriteU8((byte)SkinParts);
            stream.WriteS32V((int)MainHand);
            stream.WriteBool(EnableCensorship);
            stream.WriteBool(AllowListing);
            stream.WriteS32V((int)ParticleSettings);
        }
        internal override void Deserialize(Stream stream)
        {
            Locale = stream.ReadMinecraftString();
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