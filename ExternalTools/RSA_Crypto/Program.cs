using System;

namespace RSACryptography
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RSACryptographyKeyGenerator KeyGenerator = new RSACryptographyKeyGenerator();
            KeyGenerator.GenerateKeys(RSAKeySize.Key8192);
            Console.WriteLine(KeyGenerator.PrivateKey);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(KeyGenerator.PublicKey);

            Console.ReadLine();
            
            
            // Let's Encrypt and Decrypt password
            var textToEncrypt = "Evengyl";

            Console.WriteLine("\n Original Text: " + textToEncrypt);
            Console.WriteLine("\n-------------------------------------------------------\n");

            var encryptedText = CryptographyHelper.Encrypt(textToEncrypt, KeyGenerator.PublicKey);
            Console.WriteLine("\n Encrypted Text: " + encryptedText);
            Console.WriteLine("\n-------------------------------------------------------\n");

            var decryptedText = CryptographyHelper.Decrypt(encryptedText, KeyGenerator.PrivateKey);
            Console.WriteLine("\n Decrypted Text: " + decryptedText);

            Console.ReadKey();
        }
    }
}
