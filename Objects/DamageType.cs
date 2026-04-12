using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Primitives;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record DamageType : IRegistryEntry, INbtSerializable<DamageType>
    {
        static Identifier IRegistryEntry.RegistryId => "minecraft:damage_type";

        public string PartialTranslationKey = "default";
        public float Exhaustion = 0.0f;
        public string Scaling = "never";
        public string? Effects = null;
        public string? Type = null;
        
        public DamageType()
        {
            
        }
        
        NbtTag INbtSerializable<DamageType>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["message_id"] = (StringNbtTag)PartialTranslationKey;
            nbt["exhaustion"] = (FloatNbtTag)Exhaustion;
            nbt["scaling"] = (StringNbtTag)Scaling;
            if (Effects is not null)
            {
                nbt["effects"] = (StringNbtTag)Effects;
            }
            if (Type is not null)
            {
                nbt["death_message_type"] = (StringNbtTag)Type;
            }
            return nbt;
        }
        static DamageType INbtSerializable<DamageType>.FromNbt(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            DamageType data = new();
            data.PartialTranslationKey = compoundNbt["message_id"].As<StringNbtTag>();
            data.Exhaustion = compoundNbt["exhaustion"].As<FloatNbtTag>();
            data.Scaling = compoundNbt["scaling"].As<StringNbtTag>();
            if (compoundNbt.ContainsKey("effects"))
            {
                data.Effects = compoundNbt["effects"].As<StringNbtTag>();
            }
            if (compoundNbt.ContainsKey("death_message_type"))
            {
                data.Type = compoundNbt["death_message_type"].As<StringNbtTag>();
            }
            return data;
        }
    }
}