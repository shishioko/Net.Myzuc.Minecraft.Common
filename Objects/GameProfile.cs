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

    public struct GameProfileProperty
    {
        public string Name;
        public string Value;
        public string? Signature;
        public GameProfileProperty(string name = "", string value = "", string? signature = null)
        {
            Name = name;
            Value = value;
            Signature = signature;
        }
    }
}
