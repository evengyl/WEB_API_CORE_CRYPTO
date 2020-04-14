﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace RSACryptography
{
   public static class CryptographyHelper
    {
       private static bool _optimalAsymmetricEncryptionPadding = false;


       public static string Encrypt(string plainText, string publicKey)
       {
           int keySize = 0;
           string publicKeyXml = "";

           GetKeyFromEncryptionString(publicKey, out keySize, out publicKeyXml);

           var encrypted = Encrypt(Encoding.UTF8.GetBytes(plainText), keySize, publicKeyXml);
           
           return Convert.ToBase64String(encrypted);
       }

       private static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
       {
           if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
           int maxLength = GetMaxDataLength(keySize);
           if (data.Length > maxLength) throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");
           if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
           if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

           using (var provider = new RSACryptoServiceProvider(keySize))
           {
               provider.FromXmlString(publicKeyXml);
               return provider.Encrypt(data, _optimalAsymmetricEncryptionPadding);
           }
       }

       public static string Decrypt(string encryptedText, string privateKey)
       {
           int keySize = 0;
           string publicAndPrivateKeyXml = "";

           GetKeyFromEncryptionString(privateKey, out keySize, out publicAndPrivateKeyXml);

           var decrypted = Decrypt(Convert.FromBase64String(encryptedText), keySize, publicAndPrivateKeyXml);
       
           return Encoding.UTF8.GetString(decrypted);
       }

       private static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
       {
           if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
           if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
           if (String.IsNullOrEmpty(publicAndPrivateKeyXml)) throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");

           using (var provider = new RSACryptoServiceProvider(keySize))
           {
               provider.FromXmlString(publicAndPrivateKeyXml);
               return provider.Decrypt(data, _optimalAsymmetricEncryptionPadding);
           }
       }

       private static int GetMaxDataLength(int keySize)
       {
           if (_optimalAsymmetricEncryptionPadding)
           {
               return ((keySize - 384) / 8) + 7;
           }
           return ((keySize - 384) / 8) + 37;
       }

       private static bool IsKeySizeValid(int keySize)
       {
           return keySize >= 384 && keySize <= 16384 && keySize % 8 == 0;
       }

       private static void GetKeyFromEncryptionString(string rawkey, out int keySize, out string xmlKey)
       {
           keySize = 0;
           xmlKey = "";

           if (rawkey != null && rawkey.Length > 0)
           {
               byte[] keyBytes = Convert.FromBase64String(rawkey);
               var stringKey = Encoding.UTF8.GetString(keyBytes);

               if (stringKey.Contains("!"))
               {
                   var splittedValues = stringKey.Split(new char[] { '!' }, 2);

                   try
                   {
                       keySize = int.Parse(splittedValues[0]);
                       xmlKey = splittedValues[1];
                   }
                   catch (Exception e) { }
               }
           }
       }
    }
}
