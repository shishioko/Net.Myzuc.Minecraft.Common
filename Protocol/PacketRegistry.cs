using System.Diagnostics.Contracts;
using System.Net;
using System.Reflection;
using Net.Myzuc.Minecraft.Common.Protocol.Packets;

namespace Net.Myzuc.Minecraft.Common.Protocol
{
    public static class PacketRegistry
    {
        private static readonly Dictionary<(bool serverbound, ProtocolStage stage, int id), Type> Types = [];
        static PacketRegistry()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Packet)) && !type.IsAbstract);
            foreach (Type type in types)
            {
                Packet? instance = Activator.CreateInstance(type) as Packet;
                Contract.Assert(instance is not null);
                (bool serverbound, ProtocolStage stage, int id) signature = (instance.Serverbound, instance.ProtocolStage, instance.Id);
                Types.Add(signature, type);
            }
        }
        internal static Packet Create(bool serverbound, ProtocolStage stage, int id)
        {
            Types.TryGetValue((serverbound, stage, id), out Type? type);
            Packet? packet = type is not null ? Activator.CreateInstance(type) as Packet : null;
            if (packet is null) throw new ProtocolViolationException($"Unknown packet {(serverbound ? "Serverbound" : "Clientbound")}/{stage}/{id:X2}!");
            return packet;
        }
    }
}