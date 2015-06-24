using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using BusinessSafe.Infrastructure.Attributes;

using Microsoft.Win32;

namespace BusinessSafe.Infrastructure.Helpers
{
    [CoverageExclude]
    public static class RijndaelEncryptor
    {
        private static readonly byte[] _salt = {99, 129, 140, 172, 15, 92, 109, 1, 91, 2, 34, 238, 219};

        public static string Encrypt(string plainText)
        {
            var registryKey =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\10.0\PbsEncryptionKey");
            var password = (string)registryKey.GetValue("Key");
            var passwordDerivedBytes = new PasswordDeriveBytes(password, _salt);
            var plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            using (var memoryStream = new MemoryStream())
            {
                using (Rijndael rijndael = Rijndael.Create())
                {
                    using (
                        ICryptoTransform cryptoTransform = rijndael.CreateEncryptor(passwordDerivedBytes.GetBytes(32), rgbIV: passwordDerivedBytes.GetBytes(16)))
                    {
                        using (
                            var cryptoStream = new CryptoStream(memoryStream, cryptoTransform,
                                CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            RegistryKey registryKey =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\10.0\PbsEncryptionKey");
            var password = (string)registryKey.GetValue("Key");
            var passwordDerivedBytes = new PasswordDeriveBytes(password, _salt);
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            using (var memoryStream = new MemoryStream())
            {
                using (var rijndael = Rijndael.Create())
                {
                    using (
                        var cryptoTransform = rijndael.CreateDecryptor(passwordDerivedBytes.GetBytes(32), 
                            passwordDerivedBytes.GetBytes(16)))
                    {
                        using (
                            var cryptoStream = new CryptoStream(memoryStream, cryptoTransform,
                                CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
                            cryptoStream.Close();
                            return Encoding.Unicode.GetString(memoryStream.ToArray());
                        }
                    }
                }
            }
        }
    }
}