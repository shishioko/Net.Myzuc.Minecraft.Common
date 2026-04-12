namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record ShowRespawnScreenGameruleGameEvent : GameEvent
    {
        internal override byte Type => 11;

        public bool Enabled
        {
            get
            {
                return RawValue != 0.0f;
            }
            set
            {
                RawValue = value ? 1.0f : 0.0f;
            }
        }

        public ShowRespawnScreenGameruleGameEvent()
        {
            
        }
    }
}