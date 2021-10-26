using System;
using System.Security.Cryptography;

namespace NotificacionApp.Common
{
    /// <summary>
    /// Password helper.
    /// </summary>
    public static class EncryptionHelper
    {
        private const string Seperator = "$";
        private static readonly Random _random = new Random();

        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Creates a hash from a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string value)
        {
            // Create salt
            using var rng = new RNGCryptoServiceProvider();

            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);

            var iterations = _random.Next(1000, 10000);

            using var pbkdf2 = new Rfc2898DeriveBytes(value, salt, iterations);

            var hash = pbkdf2.GetBytes(HashSize);
            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return $"{iterations}${base64Hash}";

        }

        /// <summary>
        /// Verifies value against a hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public static bool Verify(string value, string hashedPassword)
        {
            if (!hashedPassword.Contains(Seperator))
            {
                return false;
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Split(Seperator);
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            using var pbkdf2 = new Rfc2898DeriveBytes(value, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;

        }
    }
}
