using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
#pragma warning disable SYSLIB0050

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public static class NbtSerializer
    {
        public static NbtTag Serialize<T>(T value, NbtSerializerOptions? options = null)
        {
            return Serialize(value, typeof(T), options ?? new(), 0)!;
        }
        public static NbtTag? Serialize(object? value, Type type, NbtSerializerOptions? options = null)
        {
            return Serialize(value, type, options ?? new(), 0);
        }
        public static NbtTag? Serialize(object? value, Type type, NbtSerializerOptions options, int depth)
        {
            if (depth >= options.MaxDepth) throw new SerializationException();
            depth++;
            options ??= new();
            if (value is not null && !value.GetType().IsAssignableTo(type)) throw new SerializationException("Attempted to serialize data of mismatching type!");
            if (Attribute.GetCustomAttribute(type, typeof(NbtConverterAttribute)) is NbtConverterAttribute converterAttribute)
            {
                Type converterType = converterAttribute.Type;
                NbtConverter? converter = Activator.CreateInstance(converterType) as NbtConverter;
                if (converter is null || converter.CanConvert(type)) throw new SerializationException();
                return converter.Serialize(value, options, depth);
            }
            foreach (NbtConverter converter in options.Converters)
            {
                if (!converter.CanConvert(type)) continue;
                return converter.Serialize(value, options, depth);
            }
            if (value is null) return null;
            if (type.IsPointer) throw new SerializationException("Attempted to serialize pointer type!");
            switch (value)
            {
                case NbtTag nbt: return nbt;
                case nint s0 when Marshal.SizeOf(type) == sizeof(int): return new IntNbtTag((int)s0);
                case nuint u0 when Marshal.SizeOf(type) == sizeof(int): return new IntNbtTag((int)(uint)u0);
                case nint s0: return new LongNbtTag(s0);
                case nuint u0: return new LongNbtTag((long)u0);
                case sbyte s8: return (ByteNbtTag)s8;
                case byte u8: return (ByteNbtTag)u8;
                case bool u1: return (ByteNbtTag)u1;
                case short s16: return (ShortNbtTag)s16;
                case ushort u16: return (ShortNbtTag)u16;
                case char t16: return (ShortNbtTag)t16;
                case int s32: return (IntNbtTag)s32;
                case uint u32: return (IntNbtTag)u32;
                case long s64: return (LongNbtTag)s64;
                case ulong u64: return (LongNbtTag)u64;
                case float f32: return (FloatNbtTag)f32;
                case double f64: return (DoubleNbtTag)f64;
                case decimal f128: return (DoubleNbtTag)f128;
                case string t16a: return (StringNbtTag)t16a;
                case sbyte[] s8a: return (ByteArrayNbtTag)s8a;
                case byte[] u8a: return (ByteArrayNbtTag)u8a;
                case bool[] u1a: return (ByteArrayNbtTag)u1a;
                case int[] s32a: return (IntArrayNbtTag)s32a;
                case uint[] u32a: return (IntArrayNbtTag)u32a;
                case long[] s64a: return (LongArrayNbtTag)s64a;
                case ulong[] u64a: return (LongArrayNbtTag)u64a;
                case Guid guid: return (IntArrayNbtTag)guid;
                case Enum: return Serialize(Convert.ChangeType(value, type.GetEnumUnderlyingType()), type.GetEnumUnderlyingType(), options, depth);
                case IList list:
                {
                    return new ListNbtTag(list.Cast<object?>().Select(entry => entry is not null ? Serialize(entry, entry.GetType(), options, depth) : null).Where(nbt => nbt is not null)!);
                }
                case IDictionary dictionary when dictionary.Keys is IEnumerable<string> keys:
                {
                    CompoundNbtTag compound = new();
                    foreach (string key in keys)
                    {
                        object? entry = dictionary[key];
                        if (entry is null) continue;
                        compound[key] = Serialize(entry, entry.GetType(), options, depth)!;
                    }
                    return compound;
                }
                case IEnumerable enumerable:
                {
                    return new ListNbtTag(enumerable.Cast<object?>().Select(entry => entry is not null ? Serialize(entry, entry.GetType(), options, depth) : null).Where(nbt => nbt is not null)!);
                }
                default:
                {
                    CompoundNbtTag compound = new();
                    MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                    foreach(MemberInfo member in members)
                    {
                        if (member is not PropertyInfo and not FieldInfo) continue;
                        {
                            if (member is PropertyInfo property && ((!property.CanRead && options.IgnoreReadOnlyProperties && !Attribute.IsDefined(property, typeof(NbtIncludeAttribute))) || property.GetIndexParameters().Length > 0)) continue;
                            if (member is FieldInfo field && (!field.IsInitOnly && options.IgnoreReadOnlyFields && !Attribute.IsDefined(field, typeof(NbtIncludeAttribute)))) continue;
                        }
                        Type memberType = member switch
                        {
                            PropertyInfo property => property.PropertyType,
                            FieldInfo field => field.FieldType,
                            _ => throw new SerializationException(),
                        };
                        if (!memberType.GetTypeInfo().Attributes.HasFlag(TypeAttributes.Serializable) && !memberType.IsEnum) continue; //cheap hack
                        if (Attribute.IsDefined(member, typeof(NbtIgnoreAttribute))) continue;
                        if (member is FieldInfo && !(options.IncludeFields || Attribute.IsDefined(member, typeof(NbtIncludeAttribute)))) continue;
                        string name = (Attribute.GetCustomAttribute(member, typeof(NbtPropertyAttribute)) as NbtPropertyAttribute)?.Name ?? member.Name;
                        object? instance = member switch
                        {
                            PropertyInfo property => property.GetValue(value),
                            FieldInfo field => field.GetValue(value),
                            _ => null
                        };
                        if (instance is null) continue;
                        NbtTag? nbt = Serialize(instance, instance.GetType(), options, depth);
                        if (nbt is not null) compound[name] = nbt;
                    }
                    return compound;
                }
            }
        }
        public static T? Deserialize<T>(NbtTag tag, NbtSerializerOptions? options = null)
        {
            return (T?)Deserialize(tag, typeof(T), options ?? new(), 0);
        }
        public static object? Deserialize(NbtTag tag, Type type, NbtSerializerOptions? options = null)
        {
            return Deserialize(tag, type, options ?? new(), 0);
        }
        private static object? Deserialize(NbtTag tag, Type type, NbtSerializerOptions options, int depth)
        {
            if (depth >= options.MaxDepth) throw new SerializationException();
            depth++;
            options ??= new();
            if (Attribute.GetCustomAttribute(type, typeof(NbtConverterAttribute)) is NbtConverterAttribute converterAttribute)
            {
                Type converterType = converterAttribute.Type;
                NbtConverter? converter = Activator.CreateInstance(converterType) as NbtConverter;
                if (converter is null || converter.CanConvert(type)) throw new SerializationException();
                return converter.Deserialize(tag, options, depth);
            }
            foreach (NbtConverter converter in options.Converters)
            {
                if (!converter.CanConvert(type)) continue;
                return converter.Deserialize(tag, options, depth);
            }
            if (type.IsPointer) throw new SerializationException("Attempted to serialize pointer type!");
            if (type == typeof(object)) type = typeof(NbtTag);
            if (type.IsAssignableTo(typeof(NbtTag))) return tag;
            else if (type.IsAssignableTo(typeof(nint))) return (nint)((LongNbtTag)tag).Value;
            else if (type.IsAssignableTo(typeof(nuint))) return (nuint)((LongNbtTag)tag).Value;
            else if (type.IsAssignableTo(typeof(sbyte))) return (sbyte)(ByteNbtTag)tag;
            else if (type.IsAssignableTo(typeof(byte))) return (byte)(ByteNbtTag)tag;
            else if (type.IsAssignableTo(typeof(bool))) return (bool)(ByteNbtTag)tag;
            else if (type.IsAssignableTo(typeof(short))) return (short)(ShortNbtTag)tag;
            else if (type.IsAssignableTo(typeof(ushort))) return (ushort)(ShortNbtTag)tag;
            else if (type.IsAssignableTo(typeof(char))) return (char)(ShortNbtTag)tag;
            else if (type.IsAssignableTo(typeof(int))) return (int)(IntNbtTag)tag;
            else if (type.IsAssignableTo(typeof(uint))) return (uint)(IntNbtTag)tag;
            else if (type.IsAssignableTo(typeof(long))) return (long)(LongNbtTag)tag;
            else if (type.IsAssignableTo(typeof(ulong))) return (ulong)(LongNbtTag)tag;
            else if (type.IsAssignableTo(typeof(float))) return (float)(FloatNbtTag)tag;
            else if (type.IsAssignableTo(typeof(double))) return (double)(DoubleNbtTag)tag;
            else if (type.IsAssignableTo(typeof(decimal))) return (decimal)(DoubleNbtTag)tag;
            else if (type.IsAssignableTo(typeof(string))) return (string)(StringNbtTag)tag;
            else if (type.IsAssignableTo(typeof(sbyte[]))) return (sbyte[])(ByteArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(byte[]))) return (byte[])(ByteArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(bool[]))) return (bool[])(ByteArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(int[]))) return (int[])(IntArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(uint[]))) return (uint[])(IntArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(long[]))) return (long[])(LongArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(ulong[]))) return (ulong[])(LongArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(Guid))) return (Guid)(IntArrayNbtTag)tag;
            else if (type.IsAssignableTo(typeof(Enum)))
            {
                object? underlying = Deserialize(tag, type.GetEnumUnderlyingType(), options, depth);
                return underlying is not null ? Enum.ToObject(type, underlying) : null;
            }
            else if (type.IsAssignableTo(typeof(IList)))
            {
                ListNbtTag nbtList = (ListNbtTag)tag;
                IList list = Activator.CreateInstance(type) as IList ?? throw new SerializationException();
                Type listType = type.GenericTypeArguments.ElementAtOrDefault(0) ?? typeof(object);
                foreach (NbtTag data in list)
                {
                    list.Add(Deserialize(data, listType, options, depth));
                }
                return list;
            } 
            else if (type.IsAssignableTo(typeof(IDictionary)) && (type.GenericTypeArguments.ElementAtOrDefault(0) ?? typeof(object)) == typeof(string))
            {
                CompoundNbtTag compound = tag.As<CompoundNbtTag>();
                IDictionary dictionary = Activator.CreateInstance(type) as IDictionary ?? throw new SerializationException();
                Type listType = type.GenericTypeArguments.ElementAtOrDefault(1) ?? typeof(object);
                foreach (KeyValuePair<string, NbtTag> kvp in compound)
                {
                    dictionary[kvp.Key] = Deserialize(kvp.Value, listType, options, depth);
                }
                return dictionary;
            }
            else
            {
                CompoundNbtTag compound = tag.As<CompoundNbtTag>();
                object? instance = Activator.CreateInstance(type) ?? throw new SerializationException();
                MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                foreach(MemberInfo member in members)
                {
                    if (member is not PropertyInfo and not FieldInfo) continue;
                    {
                        if (member is PropertyInfo property && (!property.CanWrite || property.GetIndexParameters().Length > 0)) continue;
                    }
                    if (Attribute.IsDefined(member, typeof(NonSerializedAttribute))) continue;
                    if (Attribute.IsDefined(member, typeof(NbtIgnoreAttribute))) continue;
                    if (member is FieldInfo && !(options.IncludeFields || Attribute.IsDefined(member, typeof(NbtIncludeAttribute)))) continue;
                    string name = (Attribute.GetCustomAttribute(member, typeof(NbtPropertyAttribute)) as NbtPropertyAttribute)?.Name ?? member.Name;
                    if (!compound.TryGetValue(name, out NbtTag? memberTag))
                    {
                        if (Attribute.IsDefined(member, typeof(NbtRequiredAttribute))) throw new SerializationException();
                        switch (options.UnmappedMemberHandling)
                        {
                            case NbtUnmappedMemberHandling.Disallow: throw new SerializationException();
                            case NbtUnmappedMemberHandling.Skip: continue;
                            default: continue;
                        }
                    }
                    object? memberInstance = member switch
                    {
                        PropertyInfo property => Deserialize(memberTag, property.PropertyType, options, depth),
                        FieldInfo field => Deserialize(memberTag, field.FieldType, options, depth),
                        _ => null
                    };
                    if (memberInstance is null) continue;
                    {
                        if (member is PropertyInfo property) property.SetValue(instance, memberInstance);
                        if (member is FieldInfo field) field.SetValue(instance, memberInstance);
                    }
                }
                return instance;
            }
        }
    }
}