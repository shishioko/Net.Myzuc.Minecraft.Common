using Net.Myzuc.Minecraft.Common.Enums;

namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record DemoMessageGameEvent : GameEvent
    {
        internal override byte Type => 5;

        public DemoMessage Message
        {
            get
            {
                return (DemoMessage)(int)RawValue;
            }
            set
            {
                RawValue = (float)(int)value;
            }
        }

        public DemoMessageGameEvent()
        {
            
        }
    }
}