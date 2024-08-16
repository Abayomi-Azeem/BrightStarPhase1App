using System.Runtime;
using System.Security.Cryptography;
using System.Text;

namespace BrightStarPhase1App.Utilities
{
    public static class SecurityUtil
    {
        public static string PasswordHasher(string password, string salt)
        {
            using (var hasher = SHA512.Create())
            {
                var passwordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Convert.ToBase64String(passwordHash);
            }
        }

        public static string GenerateSessionToken()
        {
            var guid = Guid.NewGuid();
            var token =guid.ToString().Replace("-","");
            return token;
        }
    }
}
