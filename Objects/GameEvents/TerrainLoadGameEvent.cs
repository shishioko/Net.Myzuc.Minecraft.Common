namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public sealed record TerrainLoadGameEvent : GameEvent
    {
        internal override byte Type => 13;

        public TerrainLoadGameEvent()
        {
            
        }
    }
}