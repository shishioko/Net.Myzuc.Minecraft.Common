using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Paintings
{
    public sealed record PaintingVariant : IRegistryEntry, INbtSerializable<PaintingVariant>
    {
        public static Identifier RegistryId => "minecraft:painting_variant";

        public Identifier Texture { get; set; } = new();
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public ChatComponent? Title { get; set; } = null;
        public ChatComponent? Author { get; set; } = null;

        public PaintingVariant()
        {
            
        }
        private PaintingVariant(CompoundNbtTag nbt)
        {
            Texture = nbt["asset_id"].As<StringNbtTag>().Value;
            Width = nbt["width"].As<IntNbtTag>();
            Height = nbt["height"].As<IntNbtTag>();
            if (nbt.ContainsKey("title"))
            {
                Title = Nbt.Nbt.FromNbt<ChatComponent>(nbt["title"]);
            }
            if (nbt.ContainsKey("author"))
            {
                Author = Nbt.Nbt.FromNbt<ChatComponent>(nbt["author"]);
            }
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["asset_id"] = (StringNbtTag)(string)Texture;
            nbt["width"] = (IntNbtTag)Width;
            nbt["height"] = (IntNbtTag)Height;
            if (Title is not null)
            {
                nbt["title"] = Nbt.Nbt.ToNbt(Title);
            }
            if (Author is not null)
            {
                nbt["author"] = Nbt.Nbt.ToNbt(Author);
            }
            return nbt;
        }
        public static PaintingVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}