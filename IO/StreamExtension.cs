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
            public byte[] ReadU8A()
            {
                using MemoryStream ms = new();
                stream.CopyTo(ms);
                return ms.ToArray();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<byte[]> ReadU8AAsync(CancellationToken cancellationToken = default)
            {
                using MemoryStream ms = new();
                await stream.CopyToAsync(ms, cancellationToken);
                return ms.ToArray();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte[] ReadS8A(int size)
            {
                return MemoryMarshal.Cast<byte, sbyte>(stream.ReadU8A(size)).ToArray();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8A(sbyte[] data)
            {
                stream.Write(MemoryMarshal.AsBytes(data).ToArray());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte[] ReadU8A(int size)
            {
                byte[] data = new byte[size];
                int position = 0;
                while (position < size)
                {
                    int read = stream.Read(data, position, size - position);
                    position += read;
                    if (read == 0) throw new EndOfStreamException();
                }
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU8A(byte[] data)
            {
                stream.Write(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<byte[]> ReadU8AAsync(int size, CancellationToken cancellationToken = default)
            {
                byte[] data = new byte[size];
                int position = 0;
                while (position < size)
                {
                    int read = await stream.ReadAsync(data.AsMemory(position, size - position), cancellationToken);
                    position += read;
                    if (read == 0) throw new EndOfStreamException();
                }
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteU8AAsync(byte[] data, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(data, cancellationToken);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte[] ReadS8AS32V()
            {
                return stream.ReadS8A(stream.ReadS32V());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8AS32V(sbyte[] data)
            {
                stream.WriteS32V(data.Length);
                stream.WriteS8A(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte[] ReadU8AS32V()
            {
                return stream.ReadU8A(stream.ReadS32V());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU8AS32V(byte[] data)
            {
                stream.WriteS32V(data.Length);
                stream.WriteU8A(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<byte[]> ReadU8AS32VAsync(CancellationToken cancellationToken = default)
            {
                return await stream.ReadU8AAsync(await stream.ReadS32VAsync(cancellationToken: cancellationToken), cancellationToken: cancellationToken);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteU8AS32VAsync(byte[] data, CancellationToken cancellationToken = default)
            {
                await stream.WriteS32VAsync(data.Length, cancellationToken: cancellationToken);
                await stream.WriteU8AAsync(data, cancellationToken: cancellationToken);
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
                return (stream.ReadU8()) != 0;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteBool(bool data)
            {
                stream.Write(MemoryMarshal.AsBytes<bool>(new bool[] { data }).ToArray());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte ReadS8()
            {
                return (sbyte)(stream.ReadU8A(1))[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8(sbyte data)
            {
                stream.Write(MemoryMarshal.AsBytes<sbyte>(new sbyte[] { data }).ToArray());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte ReadU8()
            {
                return stream.ReadU8A(1)[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU8(byte data)
            {
                stream.Write([data]);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async Task<byte> ReadU8Async(CancellationToken cancellationToken = default)
            {
                return (await stream.ReadU8AAsync(1, cancellationToken))[0];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public async ValueTask WriteU8Async(byte data, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[] { data }, cancellationToken);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short ReadS16()
            {
                byte[] buffer = stream.ReadU8A(sizeof(short));
                return BinaryPrimitives.ReadInt16BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS16(short data)
            {
                byte[] buffer = new byte[sizeof(short)];
                BinaryPrimitives.WriteInt16BigEndian(buffer, data);
                stream.Write(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort ReadU16()
            {
                byte[] buffer = stream.ReadU8A(sizeof(ushort));
                return BinaryPrimitives.ReadUInt16BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU16(ushort data)
            {
                byte[] buffer = new byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int ReadS32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(int));
                return BinaryPrimitives.ReadInt32BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32(int data)
            {
                byte[] buffer = new byte[sizeof(int)];
                BinaryPrimitives.WriteInt32BigEndian(buffer, data);
                stream.Write(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint ReadU32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(uint));
                return BinaryPrimitives.ReadUInt32BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU32(uint data)
            {
                byte[] buffer = new byte[sizeof(uint)];
                BinaryPrimitives.WriteUInt32BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long ReadS64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(long));
                return BinaryPrimitives.ReadInt64BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64(long data)
            {
                byte[] buffer = new byte[sizeof(long)];
                BinaryPrimitives.WriteInt64BigEndian(buffer, data);
                stream.Write(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong ReadU64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(ulong));
                return BinaryPrimitives.ReadUInt64BigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteU64(ulong data)
            {
                byte[] buffer = new byte[sizeof(ulong)];
                BinaryPrimitives.WriteUInt64BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float ReadF32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(float));
                return BinaryPrimitives.ReadSingleBigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteF32(float data)
            {
                byte[] buffer = new byte[sizeof(float)];
                BinaryPrimitives.WriteSingleBigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double ReadF64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(double));
                return BinaryPrimitives.ReadDoubleBigEndian(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteF64(double data)
            {
                byte[] buffer = new byte[sizeof(double)];
                BinaryPrimitives.WriteDoubleBigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteGuid(Guid data)
            {
                byte[] buffer = MemoryMarshal.AsBytes([data]).ToArray();
                if (!BitConverter.IsLittleEndian)
                {
                    stream.Write(buffer);
                    return;
                }
                stream.Write([buffer[3], buffer[2], buffer[1], buffer[0], buffer[5], buffer[4], buffer[7], buffer[6], buffer[8], buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15]]);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Guid ReadGuid()
            {
                byte[] buffer = stream.ReadU8A(16);
                if (BitConverter.IsLittleEndian) buffer = [buffer[3], buffer[2], buffer[1], buffer[0], buffer[5], buffer[4], buffer[7], buffer[6], buffer[8], buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15]];
                return MemoryMarshal.Cast<byte, Guid>(buffer)[0];
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ReadT16AS32V()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadS32V());
                return Encoding.UTF8.GetString(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteT16AS32V(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteS32V(buffer.Length);
                stream.WriteU8A(buffer);
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
            public int[] ReadS32A(int size)
            {
                byte[] buffer = stream.ReadU8A(size * sizeof(int));
                if (!BitConverter.IsLittleEndian) return MemoryMarshal.Cast<byte,int>(buffer).ToArray();
                Span<byte> span = buffer;
                int[] data = new int[size];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = BinaryPrimitives.ReadInt32BigEndian(span.Slice(i * sizeof(int), sizeof(int)));
                }
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32A(int[] data)
            {
                if (!BitConverter.IsLittleEndian)
                {
                    stream.Write(MemoryMarshal.AsBytes(data).ToArray());
                    return;
                }
                Span<byte> span = new byte[data.Length * sizeof(int)];
                for (int i = 0; i < data.Length; i++)
                {
                    BinaryPrimitives.WriteInt32BigEndian(span.Slice(i * sizeof(int), sizeof(int)), data[i]);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long[] ReadS64A(int size)
            {
                byte[] buffer = stream.ReadU8A(size * sizeof(long));
                if (!BitConverter.IsLittleEndian) return MemoryMarshal.Cast<byte,long>(buffer).ToArray();
                Span<byte> span = buffer;
                long[] data = new long[size];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = BinaryPrimitives.ReadInt64BigEndian(span.Slice(i * sizeof(long), sizeof(long)));
                }
                return data;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64A(long[] data)
            {
                if (!BitConverter.IsLittleEndian)
                {
                    stream.Write(MemoryMarshal.AsBytes(data).ToArray());
                    return;
                }
                Span<byte> span = new byte[data.Length * sizeof(long)];
                for (int i = 0; i < data.Length; i++)
                {
                    BinaryPrimitives.WriteInt64BigEndian(span.Slice(i * sizeof(long), sizeof(long)), data[i]);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte[] ReadS8AS32()
            {
                return stream.ReadS8A(stream.ReadS32());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS8AS32(sbyte[] data)
            {
                stream.WriteS32(data.Length);
                stream.WriteS8A(data);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ReadT16AU16()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadU16());
                return Encoding.UTF8.GetString(buffer);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteT16AU16(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteU16((ushort)buffer.Length);
                if (buffer.Length > ushort.MaxValue) stream.WriteU8A(buffer[..ushort.MaxValue]);
                else stream.WriteU8A(buffer);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int[] ReadS32AS32()
            {
                return stream.ReadS32A(stream.ReadS32());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS32AS32(int[] data)
            {
                stream.WriteS32(data.Length);
                stream.WriteS32A(data);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long[] ReadS64AS32()
            {
                return stream.ReadS64A(stream.ReadS32());
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void WriteS64AS32(long[] data)
            {
                stream.WriteS32(data.Length);
                stream.WriteS64A(data);
            }
        }
    }
}