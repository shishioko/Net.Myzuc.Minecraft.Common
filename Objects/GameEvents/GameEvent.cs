using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Objects.GameEvents
{
    public abstract record GameEvent : IBinarySerializable<GameEvent>
    {
        internal abstract byte Type { get; }
        internal float RawValue { get; set; }
        
        internal GameEvent()
        {
            
        }
        
        void IBinarySerializable<GameEvent>.Serialize(Stream stream)
        {
            stream.WriteU8(Type);
            stream.WriteF32(RawValue);
        }
        static GameEvent IBinarySerializable<GameEvent>.Deserialize(Stream stream)
        {
            byte type = stream.ReadU8();
            GameEvent data = type switch
            {
                0 => new FailedRespawnGameEvent(),
                1 => new RainStartGameEvent(),
                2 => new RainEndGameEvent(),
                3 => new GamemodeGameEvent(),
                4 => new GameEndGameEvent(),
                5 => new DemoMessageGameEvent(),
                6 => new ArrowEffectGameEvent(),
                7 => new RainStartGameEvent(),
                8 => new ThunderStrengthGameEvent(),
                9 => new PufferfishEfectGameEvent(),
                10 => new ElderGuardianEffectGameEvent(),
                11 => new ShowRespawnScreenGameruleGameEvent(),
                12 => new LimitedCraftingGameruleGameEvent(),
                13 => new TerrainLoadGameEvent(),
                _ => throw new SerializationException(),
            };
            data.RawValue = stream.ReadF32();
            return data;
        }
    }
}