using System.Security.Cryptography;

namespace RockPaperScissorsHMAC
{
    public static class HMACKeyGenerator
    {
        public static byte[] GenerateHMACKey()
        {
            byte[] bytes = new byte[16];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return bytes;
        }
    }
}