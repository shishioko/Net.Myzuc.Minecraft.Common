namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public abstract class ObjectChatComponent : ChatComponent
    {
        private readonly string Object;
        protected internal ObjectChatComponent(string @object) : base("object")
        {
            Object = @object;
        }
    }
}