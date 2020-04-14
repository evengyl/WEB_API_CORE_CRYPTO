using System;
using System.Security.Cryptography;
using System.Text;

namespace RSACryptography
{
    public enum RSAKeySize
    { 
        Key512 = 512, 
        Key1024 = 1024,
        Key2048 = 2048,
        Key4096 = 4096,
        Key8192 = 8192,
        Key16384 = 16384
    }

        

    public class RSACryptographyKeyGenerator
    {
        public string PublicKey { get; private set; }
        public string PrivateKey { get; private set; }

        public void GenerateKeys(RSAKeySize rsaKeySize)
        {
            using (var provider = new RSACryptoServiceProvider((int)rsaKeySize))
            {
                var publicKey = provider.ToXmlString(false);
                var privateKey = provider.ToXmlString(true);

                var publicKeyWithSize = IncludeKeyInEncryptionString(publicKey, (int)rsaKeySize);
                var privateKeyWithSize = IncludeKeyInEncryptionString(privateKey, (int)rsaKeySize);

                PublicKey = publicKeyWithSize;
                PrivateKey = privateKeyWithSize;
            }
        }

        private string IncludeKeyInEncryptionString(string publicKey, int keySize)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(keySize.ToString() + "!" + publicKey));
        }
    }
}
