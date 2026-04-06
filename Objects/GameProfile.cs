namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class GameProfile
    {
        public Guid uuid;
        public String username; // 16 max len

        public GameProfileProperty[] properties;
    }

    public class GameProfileProperty
    {
        public String name; // 64 max len
        public String value; // 32k max len
        public String? signature; // 1024 max len
    }
}
