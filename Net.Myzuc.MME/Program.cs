using System.Net;
using System.Net.Sockets;
using Net.Myzuc.MME.Data.Packets;
using Net.Myzuc.MME.Networking;

namespace Net.Myzuc.MME
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            Connection.OnCreate += (sender, args) =>
            {
                if (sender is not Connection connection) return;
                connection.OnPacketRead += (sender, args) =>
                {
                    Console.WriteLine(args.Packet.GetType().Name);
                    if (args.Packet is HandshakePacket handshake)
                    {
                        Console.WriteLine($"From {handshake.Address}:{handshake.Port} for {handshake.Intent}");
                    }
                };
                connection.ReadAsync().Wait();
                connection.Dispose();
            };
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 1337));
            socket.Listen();
            while (true)
            {
                Socket client = await socket.AcceptAsync();
                Connection connection = new(new NetworkStream(client, FileAccess.ReadWrite));
                Connection.RegisterConnection(connection);
            }
        }
    }
}