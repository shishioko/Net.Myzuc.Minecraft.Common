namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record ThunderStrengthGameEvent : GameEvent
    {
        internal override byte Type => 8;

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
        
        public ThunderStrengthGameEvent()
        {
            
        }
    }
}