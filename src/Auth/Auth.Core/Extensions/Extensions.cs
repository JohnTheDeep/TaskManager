using System.Security.Cryptography;
using System.Text;

namespace Auth.Core.Extensions
{
    public static class Extensions
    {
        public static string HashPassword(this string password)
        {
            var md5 = MD5.Create();

            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);

            var sb = new StringBuilder();

            foreach (var b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
