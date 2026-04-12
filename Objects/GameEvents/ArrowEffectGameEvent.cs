using Net.Myzuc.Minecraft.Common.Enums;

namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record ArrowEffectGameEvent : GameEvent
    {
        internal override byte Type => 6;

        public ArrowEffectGameEvent()
        {
            
        }
    }
}