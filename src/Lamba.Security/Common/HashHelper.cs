using System.Security.Cryptography;
using System.Text;

namespace Lamba.Security.Common
{
    public static class HashHelper
    {
        public static string ComputeHash(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var byteVal = Encoding.UTF8.GetBytes(password + salt);
            var byteHash = sha256.ComputeHash(byteVal);
            return Convert.ToBase64String(byteHash);
        }

        public static string GenerateSalt(uint size = 64)
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[size];
            rng.GetBytes(byteSalt);
            return Convert.ToBase64String(byteSalt);
        }
    }
}
