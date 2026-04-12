namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record PufferfishEfectGameEvent : GameEvent
    {
        internal override byte Type => 9;

        public PufferfishEfectGameEvent()
        {
            
        }
    }
}