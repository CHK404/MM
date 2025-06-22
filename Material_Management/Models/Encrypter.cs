using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;

namespace Material_Management.Models
{
    public class Encrypter
    {
        public static string HashPW(string pw)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pw));
                return Convert.ToBase64String(bytes);
            }
        }
        public static bool VerifyPW(string PW, string hashedPW)
        {
            string inputHash = HashPW(PW);
            return inputHash == hashedPW;
        }
    }
}
