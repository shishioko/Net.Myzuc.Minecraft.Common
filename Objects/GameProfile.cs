namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class GameProfile
    {
        public Guid uuid = Guid.Empty;
        public string username = string.Empty; // 16 max len

        public GameProfileProperty[] properties = [];

        public GameProfile()
        {
            
        }
    }

    public class GameProfileProperty
    {
        public string name = string.Empty; // 64 max len
        public string value = string.Empty; // 32k max len
        public string? signature = null; // 1024 max len

        public GameProfileProperty()
        {
            
        }
    }
}
