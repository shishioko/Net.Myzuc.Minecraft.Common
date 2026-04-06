namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class GameProfile
    {
        public struct Property
        {
            public string Name;
            public string Value;
            public string? Signature;
            public Property(string name = "", string value = "", string? signature = null)
            {
                Name = name;
                Value = value;
                Signature = signature;
            }
        }
        public Guid Guid = Guid.Empty;
        public string Name = string.Empty;
        public IEnumerable<Property> Properties = [];
        public GameProfile()
        {
            
        }
        public GameProfile(Guid id, string name)
        {
            Guid = id;
            Name = name;
        }
    }
}
