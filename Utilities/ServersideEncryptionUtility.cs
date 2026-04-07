using System.Security.Cryptography;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

namespace Net.Myzuc.Minecraft.Common.Utilities
{
    public class ServersideEncryptionUtility : IDisposable
    {
        private readonly RSA Rsa;
        private readonly byte[] Sample;
        public string ServerId { get; }
        public ServersideEncryptionUtility(string serverId = "", int sampleSize = 64)
        {
            Rsa = RSA.Create(1024);
            Sample = RandomNumberGenerator.GetBytes(sampleSize);
            ServerId = serverId;
        }
        public EncryptionRequestPacket GenerateRequest()
        {
            return new EncryptionRequestPacket()
            {
                Authenticate = false,
                ServerId = ServerId,
                PublicKey = Rsa.ExportSubjectPublicKeyInfo(),
                DecryptedSample = Sample,
            };
        }
        public byte[]? HandleResponse(EncryptionResponsePacket encryptionResponsePacket)
        {
            bool success = Enumerable.SequenceEqual(Sample, Rsa.Decrypt(encryptionResponsePacket.EncryptedSample, RSAEncryptionPadding.Pkcs1));
            byte[] secret = Rsa.Decrypt(encryptionResponsePacket.EncryptedSecret, RSAEncryptionPadding.Pkcs1);
            if (!success) throw new CryptographicException();
            return secret;
        }
        public void Dispose()
        {
            Rsa.Dispose();
        }
    }
}