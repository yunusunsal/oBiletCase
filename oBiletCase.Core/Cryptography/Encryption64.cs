using System.Security.Cryptography;
using System.Text;

namespace oBiletCase.Core.Cryptography
{
    // Şifreleme için oluşturulan static methodları içeren class.
    // DES şifreleme ve çözme yapılır.
    public class Encryption64
    {
        private static byte[] Key = new byte[0];
        private static readonly byte[] IV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
        private static readonly string EncryptionKey = "%=NS&ktns;~D7u";

        public static string Decrypt(string stringToDecrypt)
        {
            byte[] buffer = new byte[stringToDecrypt.Length];
            try
            {
               
                Key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                buffer = Convert.FromBase64String(stringToDecrypt.Replace("!","/"));
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, new DESCryptoServiceProvider().CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch (Exception exception1)
            {
                return exception1.Message;
            }
        }

        public static string Encrypt(string stringToEncrypt)
        {
            try
            {
                Key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                byte[] bytes = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, new DESCryptoServiceProvider().CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                return Convert.ToBase64String(stream.ToArray());
            }
            catch (Exception exception1)
            {
                return exception1.Message;
            }
        }


    }
}
