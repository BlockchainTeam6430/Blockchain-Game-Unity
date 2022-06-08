using System.Security.Cryptography;
using System.Text;

namespace Plugins.EntenEller.Base.Scripts.Cryptography
{
    public static class SHA1Hash
    {
        public static string Generate(byte[] data)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(data);
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
