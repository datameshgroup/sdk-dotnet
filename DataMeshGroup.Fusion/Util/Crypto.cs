using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataMeshGroup.Fusion
{
    internal static class Crypto
    {
        public static object Endocing { get; private set; }

        internal static string HashBySHA256(string source)
        {
            using (var hashSHA256Provider = new SHA256Managed())
            {
                return ByteArrayToHexString(hashSHA256Provider.ComputeHash(Encoding.UTF8.GetBytes(source)));
            }
        }

        internal static string EncryptWithTripleDES(string source, string key)
        {
            using (var desCryptoProvider = new TripleDESCryptoServiceProvider
            {
                Key = HexStringToByteArray(key),
                IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            })
            {

                var byteBuff = HexStringToByteArray(source);
                return ByteArrayToHexString(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
        }

        internal static string DecryptWithTripleDES(string encodedText, string key)
        {
            using (var desCryptoProvider = new TripleDESCryptoServiceProvider
            {
                Key = HexStringToByteArray(key),
                IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            })
            { 

                var byteBuff = HexStringToByteArray(encodedText);
            return ByteArrayToHexString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
        }

        internal static string GenerateKey()
        {
            var rng = new RNGCryptoServiceProvider();
            using (rng)
            {
                var key = new byte[16];
                rng.GetBytes(key);

                for (var i = 0; i < key.Length; ++i)
                {
                    int keyByte = key[i] & 0xFE;
                    var parity = 0;
                    for (var b = keyByte; b != 0; b >>= 1) parity ^= b & 1;
                    key[i] = (byte)(keyByte | (parity == 0 ? 1 : 0));
                }

                return ByteArrayToHexString(key);
            }
        }

        internal static string ByteArrayToHexString(byte[] ba)
        {
            var hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        internal static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
