using System.Buffers.Binary;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace Net.Myzuc.Minecraft.Common.IO
{
    internal static class StreamExtension
    {
        extension(Stream stream)
        {
            public byte[] ReadU8A()
            {
                using MemoryStream ms = new();
                stream.CopyTo(ms);
                return ms.ToArray();
            }
            public async Task<byte[]> ReadU8AAsync(CancellationToken cancellationToken = default)
            {
                using MemoryStream ms = new();
                await stream.CopyToAsync(ms, cancellationToken);
                return ms.ToArray();
            }
            
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
            public void WriteU8A(byte[] data)
            {
                stream.Write(data);
            }
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
            public async ValueTask WriteU8AAsync(byte[] data, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(data, cancellationToken);
            }
            
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
            
            public bool ReadBool()
            {
                return (stream.ReadU8()) != 0;
            }
            public void WriteBool(bool data)
            {
                stream.Write(MemoryMarshal.AsBytes<bool>(new bool[] { data }).ToArray());
            }
            
            public sbyte ReadS8()
            {
                return (sbyte)(stream.ReadU8A(1))[0];
            }
            public void WriteS8(sbyte data)
            {
                stream.Write(MemoryMarshal.AsBytes<sbyte>(new sbyte[] { data }).ToArray());
            }
            public byte ReadU8()
            {
                return stream.ReadU8A(1)[0];
            }
            public void WriteU8(byte data)
            {
                stream.Write([data]);
            }
            public async Task<byte> ReadU8Async(CancellationToken cancellationToken = default)
            {
                return (await stream.ReadU8AAsync(1, cancellationToken))[0];
            }
            public async ValueTask WriteU8Async(byte data, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[] { data }, cancellationToken);
            }
            
            public short ReadS16()
            {
                byte[] buffer = stream.ReadU8A(sizeof(short));
                return BinaryPrimitives.ReadInt16BigEndian(buffer);
            }
            public void WriteS16(short data)
            {
                byte[] buffer = new byte[sizeof(short)];
                BinaryPrimitives.WriteInt16BigEndian(buffer, data);
                stream.Write(buffer);
            }
            public ushort ReadU16()
            {
                byte[] buffer = stream.ReadU8A(sizeof(ushort));
                return BinaryPrimitives.ReadUInt16BigEndian(buffer);
            }
            public void WriteU16(ushort data)
            {
                byte[] buffer = new byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            public int ReadS32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(int));
                return BinaryPrimitives.ReadInt32BigEndian(buffer);
            }
            public void WriteS32(int data)
            {
                byte[] buffer = new byte[sizeof(int)];
                BinaryPrimitives.WriteInt32BigEndian(buffer, data);
                stream.Write(buffer);
            }
            public uint ReadU32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(uint));
                return BinaryPrimitives.ReadUInt32BigEndian(buffer);
            }
            public void WriteU32(uint data)
            {
                byte[] buffer = new byte[sizeof(uint)];
                BinaryPrimitives.WriteUInt32BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            public long ReadS64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(long));
                return BinaryPrimitives.ReadInt64BigEndian(buffer);
            }
            public void WriteS64(long data)
            {
                byte[] buffer = new byte[sizeof(long)];
                BinaryPrimitives.WriteInt64BigEndian(buffer, data);
                stream.Write(buffer);
            }
            public ulong ReadU64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(ulong));
                return BinaryPrimitives.ReadUInt64BigEndian(buffer);
            }
            public void WriteU64(ulong data)
            {
                byte[] buffer = new byte[sizeof(ulong)];
                BinaryPrimitives.WriteUInt64BigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            public float ReadF32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(float));
                return BinaryPrimitives.ReadSingleBigEndian(buffer);
            }
            public void WriteF32(float data)
            {
                byte[] buffer = new byte[sizeof(float)];
                BinaryPrimitives.WriteSingleBigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            public double ReadF64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(double));
                return BinaryPrimitives.ReadDoubleBigEndian(buffer);
            }
            public void WriteF64(double data)
            {
                byte[] buffer = new byte[sizeof(double)];
                BinaryPrimitives.WriteDoubleBigEndian(buffer, data);
                stream.Write(buffer);
            }
            
            public string ReadT16AS32V()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadS32V());
                return Encoding.UTF8.GetString(buffer);
            }
            public void WriteT16AS32V(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteS32V(buffer.Length);
                stream.WriteU8A(buffer);
            }
            
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
            public Guid ReadGuid()
            {
                byte[] buffer = stream.ReadU8A(16);
                if (BitConverter.IsLittleEndian) buffer = [buffer[3], buffer[2], buffer[1], buffer[0], buffer[5], buffer[4], buffer[7], buffer[6], buffer[8], buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15]];
                return MemoryMarshal.Cast<byte, Guid>(buffer)[0];
            }
        }
    }
}