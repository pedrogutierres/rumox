using Konscious.Security.Cryptography;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Identity
{
    public static class PasswordHelper
    {
        public static byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        public static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 2, // cores
                Iterations = 25,
                MemorySize = 1024 * 12 // 12 MB
            };

            return argon2.GetBytes(256);
        }

        public static bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }
}
