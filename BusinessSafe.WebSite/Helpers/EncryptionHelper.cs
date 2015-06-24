using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BusinessSafe.WebSite.Helpers
{
    public class EncryptionHelper
    {
        private const string ENCRYPTION_KEY = "key";
        private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString(CultureInfo.InvariantCulture));

        public static string Encrypt(string inputText)
        {
            var rijndaelCipher = new RijndaelManaged();
            var plainText = Encoding.Unicode.GetBytes(inputText);
            var secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        var base64 = Convert.ToBase64String(memoryStream.ToArray());
                        return base64;
                    }
                }
            }
        }

        public static string Decrypt(string inputText)
        {
            var rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(inputText);
            var secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream(encryptedData))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainText = new byte[encryptedData.Length];
                        var decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        public static NameValueCollection DecryptQueryString(String rawUrl)
        {
            var index = rawUrl.IndexOf("?", StringComparison.Ordinal) + 1;
            var encryptedQueryString = rawUrl.Replace("enc=", string.Empty).Substring(index);

            if (encryptedQueryString.Split('&').Length > 0)
            {
                encryptedQueryString = encryptedQueryString.Split('&')[0];
            }

            var decryptedQueryString = Decrypt(encryptedQueryString);
            var values = decryptedQueryString.Split('&');
            var decryptedQueryStringValues = new NameValueCollection();

            foreach (var v in values)
            {
                decryptedQueryStringValues.Add(v.Split('=')[0], v.Split('=')[1]);
            }

            return decryptedQueryStringValues;
        }
    }
}