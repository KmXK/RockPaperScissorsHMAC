using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissorsHMAC
{
    public static  class HMACGenerator
    {
        public static string CreateHMAC(byte[] key, string message)
        {
            HMAC hmac = HMAC.Create(nameof(HMACSHA256));
            hmac.Key = key;

            byte[] result = hmac.ComputeHash(Encoding.Default.GetBytes(message));
            return BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
        }
    }
}