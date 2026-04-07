using System.Text;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.IO
{
    internal static class StreamExtension
    {
        extension(Stream stream)
        {
            public string ReadMinecraftString()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadS32V());
                return Encoding.UTF8.GetString(buffer);
            }
            public void WriteMinecraftString(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteS32V(buffer.Length);
                stream.WriteU8A(buffer);
            }
        }
    }
}