namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record RainStrengthGameEvent : GameEvent
    {
        internal override byte Type => 7;

        public float Strength
        {
            get
            {
                return RawValue;
            }
            set
            {
                RawValue = value;
            }
        }
        
        public RainStrengthGameEvent()
        {
            
        }
    }
}