namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record FailedRespawnGameEvent : GameEvent
    {
        internal override byte Type => 0;
        
        public FailedRespawnGameEvent()
        {
            
        }
    }
}