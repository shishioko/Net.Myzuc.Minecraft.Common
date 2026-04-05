using System;

namespace Net.Myzuc.Minecraft.Common
{
    public static class Minecraft
    {
        public static Version Version { get; } = new(1, 21, 11, 0);
        public static int ProtocolVersion { get; } = 774;
    }
}