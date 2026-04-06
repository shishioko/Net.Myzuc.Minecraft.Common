namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class GameProfile
    {
        public Guid Guid = Guid.Empty;
        public string Name = string.Empty;

        public GameProfileProperty[] Properties = [];

        public GameProfile()
        {
            
        }
    }

    public class GameProfileProperty
    {
        public string Name = string.Empty;
        public string Value = string.Empty;
        public string? Signature = null;

        public GameProfileProperty()
        {
            
        }
    }
}
