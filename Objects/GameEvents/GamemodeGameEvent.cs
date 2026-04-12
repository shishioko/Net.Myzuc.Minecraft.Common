using Net.Myzuc.Minecraft.Common.Enums;

namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record GamemodeGameEvent : GameEvent
    {
        internal override byte Type => 3;

        public Gamemode Gamemode
        {
            get
            {
                return (Gamemode)(sbyte)RawValue;
            }
            set
            {
                RawValue = (float)(sbyte)value;
            }
        }

        public GamemodeGameEvent()
        {
            
        }
    }
}