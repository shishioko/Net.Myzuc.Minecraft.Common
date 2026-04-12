using Net.Myzuc.Minecraft.Common.Enums;

namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record GameEndGameEvent : GameEvent
    {
        internal override byte Type => 4;

        public bool PlayCredits
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

        public GameEndGameEvent()
        {
            
        }
    }
}