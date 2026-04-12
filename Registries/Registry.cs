using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Registries
{
    public sealed record Registry<T> : IBinarySerializable<Registry<T>>, IRegistry where T : INbtSerializable<T>
    {
        public Identifier Id { get; set; } = new();
        public OrderedDictionary<Identifier, T?> Content { get; set; } = new();
        
        public Registry()
        {
            
        }
        public Registry(Identifier id)
        {
            Id = id;
        }
        public Registry(Identifier id, OrderedDictionary<Identifier, T?> content)
        {
            Id = id;
            Content = content;
        }
        
        public static IRegistry Decode(Registry<NbtTag> registry)
        {
            Registry<T> decoded = new();
            decoded.Id = registry.Id;
            decoded.Content = new(registry.Content.Select(
                kvp => new KeyValuePair<Identifier, T?>(kvp.Key, Nbt.Nbt.FromNullableNbt<T>(kvp.Value)))
            );
            return decoded;
        }
        public Registry<NbtTag> Encode()
        {
            Registry<NbtTag> encoded = new();
            encoded.Id = Id;
            encoded.Content = new(Content.Select(
                kvp => new KeyValuePair<Identifier, NbtTag?>(kvp.Key, Nbt.Nbt.ToNullableNbt(kvp.Value)))
            );
            return encoded;
        }
        void IBinarySerializable<Registry<T>>.Serialize(Stream stream)
        {
            stream.Write(Id);
            stream.WriteS32V(Content.Count);
            foreach (KeyValuePair<Identifier, T?> kvp in Content)
            {
                stream.Write(kvp.Key);
                stream.WriteBool(kvp.Value is not null);
                if (kvp.Value is not null)
                {
                    stream.WriteNbt(kvp.Value);
                }
            };
        }
        static Registry<T> IBinarySerializable<Registry<T>>.Deserialize(Stream stream)
        {
            Registry<T> registry = new();
            registry.Id = stream.Read<Identifier>();
            int entries = stream.ReadS32V();
            for (int i = 0; i < entries; i++)
            {
                Identifier entryId = stream.Read<Identifier>();
                T? entry = default;
                if (stream.ReadBool())
                {
                    entry = stream.ReadNbt<T>();
                }
                registry.Content.SetAt(i, entryId, entry);
            }
            return registry;
        }
    }
}