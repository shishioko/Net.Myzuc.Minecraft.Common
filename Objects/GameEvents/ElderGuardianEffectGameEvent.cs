namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record ElderGuardianEffectGameEvent : GameEvent
    {
        internal override byte Type => 10;

        public ElderGuardianEffectGameEvent()
        {
            
        }
    }
}