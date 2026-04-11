using System.Buffers.Binary;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Net.Myzuc.Minecraft.Common.Nbt;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.IO
{
    internal static class StreamExtension
    {
        extension(Stream stream)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<byte> ReadU8A()
            {
                using MemoryStream ms = new();
                stream.CopyTo(ms);
                return ms.ToArray().AsSpan();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<Memory<byte>> ReadU8AAsync(CancellationToken cancellationToken = default)
            {
                using MemoryStream ms = new();
                await stream.CopyToAsync(ms, cancellationToken);
                return ms.ToArray().AsMemory();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<sbyte> ReadS8A(int size)
            {
                return MemoryMarshal.Cast<byte, sbyte>(stream.ReadU8A(size));
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8A(ReadOnlySpan<sbyte> data)
            {
                stream.Write(MemoryMarshal.AsBytes(data));
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<byte> ReadU8A(int size)
            {
                Span<byte> data = new(new byte[size]);
                stream.ReadExactly(data);
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU8A(ReadOnlySpan<byte> data)
            {
                stream.Write(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<Memory<byte>> ReadU8AAsync(int size, CancellationToken cancellationToken = default)
            {
                Memory<byte> data = new(new byte[size]);
                await stream.ReadExactlyAsync(data, cancellationToken);
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteU8AAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(data, cancellationToken);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<sbyte> ReadS8AS32V()
            {
                return stream.ReadS8A(stream.ReadS32V());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8AS32V(ReadOnlySpan<sbyte> data)
            {
                stream.WriteS32V(data.Length);
                stream.WriteS8A(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<byte> ReadU8AS32V()
            {
                return stream.ReadU8A(stream.ReadS32V());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU8AS32V(ReadOnlySpan<byte> data)
            {
                stream.WriteS32V(data.Length);
                stream.WriteU8A(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<Memory<byte>> ReadU8AS32VAsync(CancellationToken cancellationToken = default)
            {
                return await stream.ReadU8AAsync(await stream.ReadS32VAsync(cancellationToken), cancellationToken);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteU8AS32VAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
            {
                await stream.WriteS32VAsync(data.Length, cancellationToken);
                await stream.WriteU8AAsync(data, cancellationToken);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int ReadS32V()
            {
                uint data = 0;
                int position = 0;
                while (true)
                {
                    byte current = stream.ReadU8();
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return (int)data;
                    position += 7;
                    if (position >= sizeof(uint) * 8) throw new SerializationException();
                }
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32V(int data)
            {
                uint state = (uint)data;
                do
                {
                    byte current = (byte)state;
                    state >>= 7;
                    if (state != 0) current |= 128;
                    else current &= 127;
                    stream.WriteU8(current);
                }
                while (state != 0);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<int> ReadS32VAsync(CancellationToken cancellationToken = default)
            {
                uint data = 0;
                int position = 0;
                while (true)
                {
                    byte current = await stream.ReadU8Async(cancellationToken);
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return (int)data;
                    position += 7;
                    if (position >= sizeof(uint) * 8) throw new SerializationException();
                }
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteS32VAsync(int data, CancellationToken cancellationToken = default)
            {
                uint state = (uint)data;
                do
                {
                    byte current = (byte)state;
                    state >>= 7;
                    if (state != 0) current |= 128;
                    else current &= 127;
                    await stream.WriteU8Async(current, cancellationToken);
                }
                while (state != 0);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long ReadS64V()
            {
                ulong data = 0;
                int position = 0;
                while (true)
                {
                    byte current = stream.ReadU8();
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return (long)data;
                    position += 7;
                    if (position >= 64) throw new SerializationException();
                }
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64V(long data)
            {
                ulong state = (ulong)data;
                int size = 0;
                do
                {
                    byte current = (byte)state;
                    state >>= 7;
                    if (state != 0) current |= 128;
                    else current &= 127;
                    stream.WriteU8(current);
                    size++;
                }
                while (state != 0);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool ReadBool()
            {
                return stream.ReadU8() != 0;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteBool(bool data)
            {
                stream.WriteU8((byte)(data ? 1 : 0));
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte ReadS8()
            {
                return stream.ReadS8A(1)[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8(sbyte data)
            {
                stream.WriteS8A(stackalloc sbyte[1] { data });
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte ReadU8()
            {
                return stream.ReadU8A(1)[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU8(byte data)
            {
                stream.WriteU8A(stackalloc byte[1] { data });
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<byte> ReadU8Async(CancellationToken cancellationToken = default)
            {
                return (await stream.ReadU8AAsync(1, cancellationToken)).Span[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteU8Async(byte data, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[1] { data }, cancellationToken);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short ReadS16()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(short));
                return BinaryPrimitives.ReadInt16BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS16(short data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(short)];
                BinaryPrimitives.WriteInt16BigEndian(buffer, data);
                stream.Write(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort ReadU16()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(ushort));
                return BinaryPrimitives.ReadUInt16BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU16(ushort data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int ReadS32()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(int));
                return BinaryPrimitives.ReadInt32BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32(int data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(int)];
                BinaryPrimitives.WriteInt32BigEndian(buffer, data);
                stream.Write(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint ReadU32()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(uint));
                return BinaryPrimitives.ReadUInt32BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU32(uint data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(uint)];
                BinaryPrimitives.WriteUInt32BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long ReadS64()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(long));
                return BinaryPrimitives.ReadInt64BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64(long data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(long)];
                BinaryPrimitives.WriteInt64BigEndian(buffer, data);
                stream.Write(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong ReadU64()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(ulong));
                return BinaryPrimitives.ReadUInt64BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU64(ulong data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(ulong)];
                BinaryPrimitives.WriteUInt64BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float ReadF32()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(float));
                return BinaryPrimitives.ReadSingleBigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteF32(float data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(float)];
                BinaryPrimitives.WriteSingleBigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double ReadF64()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(sizeof(double));
                return BinaryPrimitives.ReadDoubleBigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteF64(double data)
            {
                Span<byte> buffer = stackalloc byte[sizeof(double)];
                BinaryPrimitives.WriteDoubleBigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Guid ReadGuid()
            {
                ReadOnlySpan<byte> buffer = stream.ReadU8A(16);
                if (!BitConverter.IsLittleEndian) return MemoryMarshal.Cast<byte, Guid>(buffer)[0];
                return MemoryMarshal.Cast<byte, Guid>(stackalloc byte[16] { buffer[3], buffer[2], buffer[1], buffer[0], buffer[5], buffer[4], buffer[7], buffer[6], buffer[8], buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15] })[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteGuid(Guid data)
            {
                ReadOnlySpan<byte> buffer = MemoryMarshal.AsBytes(stackalloc Guid[1] { data });
                if (!BitConverter.IsLittleEndian) stream.Write(buffer);
                else stream.Write(stackalloc byte[16] { buffer[3], buffer[2], buffer[1], buffer[0], buffer[5], buffer[4], buffer[7], buffer[6], buffer[8], buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15] });
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ReadT16AS32V()
            {
                return Encoding.UTF8.GetString(stream.ReadU8AS32V());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteT16AS32V(string data)
            {
                stream.WriteU8AS32V(Encoding.UTF8.GetBytes(data));
            }
            
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TBinarySerializable Read<TBinarySerializable>() where TBinarySerializable : IBinarySerializable<TBinarySerializable>
            {
                return TBinarySerializable.Deserialize(stream);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Write<TBinarySerializable>(TBinarySerializable data) where TBinarySerializable : IBinarySerializable<TBinarySerializable>
            {
                TBinarySerializable.Serialize(data, stream);
            }
            
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public NbtTag? ReadNbt()
            {
                return Nbt.Nbt.Deserialize(stream);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteNbt(NbtTag? data)
            {
                Nbt.Nbt.Serialize(data, stream);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TNbtSerializable ReadNbt<TNbtSerializable>() where TNbtSerializable : INbtSerializable<TNbtSerializable>
            {
                return TNbtSerializable.FromNbt(stream.ReadNbt() ?? throw new SerializationException());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteNbt<TNbtSerializable>(TNbtSerializable data) where TNbtSerializable : INbtSerializable<TNbtSerializable>
            {
                stream.WriteNbt(TNbtSerializable.ToNbt(data));
            }
            
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<int> ReadS32A(int size)
            {
                Span<byte> buffer = stream.ReadU8A(size * sizeof(int));
                Span<int> data = MemoryMarshal.Cast<byte, int>(buffer);
                if (BitConverter.IsLittleEndian) BinaryPrimitives.ReverseEndianness(data, data);
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32A(ReadOnlySpan<int> data)
            {
                if(!BitConverter.IsLittleEndian)
                {
                    stream.WriteU8A(MemoryMarshal.AsBytes(data));
                    return;
                }
                Span<int> reversed = new(new int[data.Length]);
                BinaryPrimitives.ReverseEndianness(data, reversed);
                stream.WriteU8A(MemoryMarshal.AsBytes(reversed));
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<long> ReadS64A(int size)
            {
                Span<byte> buffer = stream.ReadU8A(size * sizeof(long));
                Span<long> data = MemoryMarshal.Cast<byte, long>(buffer);
                if (BitConverter.IsLittleEndian) BinaryPrimitives.ReverseEndianness(data, data);
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64A(ReadOnlySpan<long> data)
            {
                if(!BitConverter.IsLittleEndian)
                {
                    stream.WriteU8A(MemoryMarshal.AsBytes(data));
                    return;
                }
                Span<long> reversed = new(new long[data.Length]);
                BinaryPrimitives.ReverseEndianness(data, reversed);
                stream.WriteU8A(MemoryMarshal.AsBytes(reversed));
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<sbyte> ReadS8AS32()
            {
                return stream.ReadS8A(stream.ReadS32());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8AS32(ReadOnlySpan<sbyte> data)
            {
                stream.WriteS32(data.Length);
                stream.WriteS8A(data);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ReadT16AU16()
            {
                Span<byte> buffer = stream.ReadU8A(stream.ReadU16());
                return Encoding.UTF8.GetString(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteT16AU16(string data)
            {
                Span<byte> buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteU16((ushort)buffer.Length);
                if (buffer.Length > ushort.MaxValue) stream.WriteU8A(buffer[..ushort.MaxValue]);
                else stream.WriteU8A(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<int> ReadS32AS32()
            {
                return stream.ReadS32A(stream.ReadS32());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32AS32(ReadOnlySpan<int> data)
            {
                stream.WriteS32(data.Length);
                stream.WriteS32A(data);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<long> ReadS64AS32()
            {
                return stream.ReadS64A(stream.ReadS32());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64AS32(ReadOnlySpan<long> data)
            {
                stream.WriteS32(data.Length);
                stream.WriteS64A(data);
            }
        }
    }
}