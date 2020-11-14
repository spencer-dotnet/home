using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Home.Shared
{
    public sealed class PasswordHash
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 1000;
        readonly byte[] _salt, _hash;

        public PasswordHash(string password)
        {
            
        }

        public static string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPassword(string password, string hash)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hashedPass = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                if(hashedPass == hash)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
