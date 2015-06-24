using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace BusinessSafe.Infrastructure.Security
{
    public class RijndaelEncryptor : IEncryptor
    {
        private byte[] _salt = { 99, 129, 140, 172, 15, 92, 109, 1, 91, 2, 34, 238, 219 };

        public string Encrypt(string plainText)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\10.0\PbsEncryptionKey");
            string password = (string)registryKey.GetValue("Key");
            PasswordDeriveBytes passwordDerivedBytes = new PasswordDeriveBytes(password, _salt);
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            using (MemoryStream memoryStream = new MemoryStream())
            using (Rijndael rijndael = Rijndael.Create())
            using (ICryptoTransform cryptoTransform = rijndael.CreateEncryptor(passwordDerivedBytes.GetBytes(32), passwordDerivedBytes.GetBytes(16)))
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public string Decrypt(string encryptedText)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\10.0\PbsEncryptionKey");
            string password = (string)registryKey.GetValue("Key");
            PasswordDeriveBytes passwordDerivedBytes = new PasswordDeriveBytes(password, _salt);
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            using (MemoryStream memoryStream = new MemoryStream())
            using (Rijndael rijndael = Rijndael.Create())
            using (ICryptoTransform cryptoTransform = rijndael.CreateDecryptor(passwordDerivedBytes.GetBytes(32), passwordDerivedBytes.GetBytes(16)))
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
                cryptoStream.Close();
                return Encoding.Unicode.GetString(memoryStream.ToArray());
            }
        }
    }
}
