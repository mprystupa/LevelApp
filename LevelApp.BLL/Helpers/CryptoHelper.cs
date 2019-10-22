using System;
using System.Security.Cryptography;

namespace LevelApp.BLL.Helpers
{
    public static class CryptoHelper
    {
        /// <summary>
        /// Generates hash and salt used in hashing for passed password string.
        /// </summary>
        /// <param name="password">Password to hash.</param>
        /// <returns>Tuple containing password hash and salt used to hash the password.</returns>
        public static (string PasswordHash, string PasswordSalt) GeneratePasswordHashData(string password)
        {
            var passwordSalt = GeneratePasswordSalt();
            var passwordHash = HashPassword(password, passwordSalt);

            return (Convert.ToBase64String(passwordHash), Convert.ToBase64String(passwordSalt));
        }
        
        /// <summary>
        /// Validates whether passed password string is the same as used to create stored password hash.
        /// </summary>
        /// <param name="passwordToValidate"></param>
        /// <param name="passwordHashToCompare"></param>
        /// <param name="passwordSalt"></param>
        /// <returns>Returns true when password is valid.</returns>
        /// <returns>Returns true when password is valid.</returns>
        public static bool ValidatePassword(string passwordToValidate, string passwordHashToCompare, string passwordSalt)
        {
            var saltByteArray = Convert.FromBase64String(passwordSalt);
            var passwordHashByteArray = Convert.FromBase64String(passwordHashToCompare);
            var passwordHashToValidate = HashPassword(passwordToValidate, saltByteArray);

            for (var i = 0; i <= passwordHashToValidate.Length; i++)
            {
                if (passwordHashByteArray[i] != passwordHashToCompare[i])
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Generates Base64 string containing random salt to concatenate with password.
        /// </summary>
        /// <returns></returns>
        private static byte[] GeneratePasswordSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }
        
        /// <summary>
        /// Hashes and salts passed password text.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>Hashed password as Base64 string.</returns>
        private static byte[] HashPassword(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);

            return hash;
        }
    }
}