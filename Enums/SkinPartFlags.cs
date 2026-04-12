namespace Net.Myzuc.Minecraft.Common.Enums
{
    [Flags]
    public enum SkinPartFlags : byte
    {
        Cape = 0x01,
        Jacket = 0x02,
        LeftSleeve = 0x04,
        RightSleeve = 0x08,
        LeftLeg = 0x10,
        RightLeg = 0x20,
        Hat = 0x40,
        None = 0x00,
        All = 0x7F
    }
}