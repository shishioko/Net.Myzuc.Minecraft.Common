namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record RainStartGameEvent : GameEvent
    {
        internal override byte Type => 1;
        
        public RainStartGameEvent()
        {
            
        }
    }
}