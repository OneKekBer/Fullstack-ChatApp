using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Business.Helpers
{
    public class HashHelper
    {
        public static string ConvertPasswordToHash(string password)
        {
            return Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }

        public static bool IsPasswordHashesEquals(string oldHashPassword, string incomePassword)
        {
            if (oldHashPassword == ConvertPasswordToHash(incomePassword))
                return true; // Don't forget about spaces between code-blocks

            return false;
        }
    }
}
