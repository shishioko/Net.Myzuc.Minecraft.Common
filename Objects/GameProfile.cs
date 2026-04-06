namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class GameProfile
    {
        public Guid Guid = Guid.Empty;
        public string Name = string.Empty; // 16 max len

        public GameProfileProperty[] Properties = [];

        public GameProfile()
        {
            
        }
    }

    public class GameProfileProperty
    {
        public string Name = string.Empty; // 64 max len
        public string Value = string.Empty; // 32k max len
        public string? Signature = null; // 1024 max len

        public GameProfileProperty()
        {
            
        }
    }
}
