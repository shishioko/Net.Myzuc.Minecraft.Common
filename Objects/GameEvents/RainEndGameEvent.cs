namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record RainEndGameEvent : GameEvent
    {
        internal override byte Type => 2;
        
        public RainEndGameEvent()
        {
            
        }
    }
}