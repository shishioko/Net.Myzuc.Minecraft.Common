namespace Net.Myzuc.Minecraft.Common.Protocol
{
    public enum ProtocolStage
    {
        Disconnected,
        Handshake,
        Status,
        Login,
        Configuration,
        Play,
    }
}