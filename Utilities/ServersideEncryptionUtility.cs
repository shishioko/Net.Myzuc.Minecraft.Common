using System.Data;
using System.Net;
using System.Numerics;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Objects;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

namespace Net.Myzuc.Minecraft.Common.Utilities
{
    public class ServersideEncryptionUtility : IDisposable
    {
        private readonly RSA Rsa;
        private readonly byte[] Sample;
        private byte[]? Secret = null;
        private string Username { get; }
        private string ServerId { get; }
        public bool Authenticate { get; }
        public ServersideEncryptionUtility(bool authenticate, string username, string serverId = "", int sampleSize = 64)
        {
            Rsa = RSA.Create(1024);
            Sample = RandomNumberGenerator.GetBytes(sampleSize);
            ServerId = serverId;
            Username = username;
            Authenticate = authenticate;
        }
        public EncryptionRequestPacket GenerateRequest()
        {
            return new EncryptionRequestPacket()
            {
                Authenticate = Authenticate,
                ServerId = ServerId,
                PublicKey = Rsa.ExportSubjectPublicKeyInfo(),
                DecryptedSample = Sample,
            };
        }
        public byte[] HandleResponse(EncryptionResponsePacket encryptionResponsePacket)
        {
            if (Secret is not null) throw new InvalidOperationException();
            bool success = Enumerable.SequenceEqual(Sample, Rsa.Decrypt(encryptionResponsePacket.EncryptedSample, RSAEncryptionPadding.Pkcs1));
            Secret = Rsa.Decrypt(encryptionResponsePacket.EncryptedSecret, RSAEncryptionPadding.Pkcs1);
            if (!success) throw new CryptographicException();
            return Secret;
        }
        public void Dispose()
        {
            Rsa.Dispose();
        }
        public async Task<GameProfile> AuthenticateAsync(IPAddress? ip = null)
        {
            if (!Authenticate) throw new InvalidOperationException();
            byte[] hash = SHA1.HashData(Encoding.ASCII.GetBytes(ServerId).Concat(Secret ?? []).Concat(Rsa.ExportSubjectPublicKeyInfo()).ToArray());
            Array.Reverse(hash);
            BigInteger number = new BigInteger(hash);
            string hashstring = (number < 0 ? "-" + (-number).ToString("x") : number.ToString("x")).TrimStart('0');
            using HttpClient http = new();
            HttpResponseMessage auth = await http.GetAsync($"https://sessionserver.mojang.com/session/minecraft/hasJoined?username={Username}&serverId={hashstring}{(ip is not null ? $"&ip={ip}" : string.Empty)}");
            if (auth.StatusCode != HttpStatusCode.OK) throw new AuthenticationException();
            GameProfile? profile = await JsonSerializer.DeserializeAsync<GameProfile>(await auth.Content.ReadAsStreamAsync(), Global.JsonSerializerOptions);
            if (profile is null) throw new NoNullAllowedException();
            return profile;
        }
    }
}