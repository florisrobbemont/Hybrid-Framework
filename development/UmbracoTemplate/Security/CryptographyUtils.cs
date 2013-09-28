using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace UmbracoTemplate.Security
{
    /// <summary>
    /// Helpers methods for encryption/decryption
    /// </summary>
    internal static class CryptographyUtils
    {
        private const string SaltValue = "6d95b261-4a35-4d54-b001-cd31e79e00f2"; 

        /// <summary>
        /// Generates a random salt value
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomSaltBytes()
        {
            var numArray = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(numArray);

            return numArray;
        }

        /// <summary>
        /// Generates a random salt value as a Base64 string
        /// </summary>
        public static string GenerateRandomSaltString()
        {
            return Convert.ToBase64String(GenerateRandomSaltBytes());
        }

        /// <summary>
        /// Generates a salt byte array based on the <c>Rfc2898DeriveBytes</c> class
        /// </summary>
        /// <param name="value">The string to use a the password for the salt</param>
        /// <returns></returns>
        public static byte[] GenerateSaltBytesFromString(string value)
        {
            using (
                var hasher = new Rfc2898DeriveBytes(value + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture),
                                                    Encoding.ASCII.GetBytes(SaltValue), 1000))
            {
                return hasher.GetBytes(32);    
            }
            
        }

        /// <summary>
        /// Generates a salt string based on the <c>Rfc2898DeriveBytes</c> class
        /// </summary>
        /// <param name="value">The string to use a the password for the salt</param>
        /// <returns></returns>
        public static string GenerateSaltStringFromString(string value)
        {
            return Convert.ToBase64String(GenerateSaltBytesFromString(value));
        }

        /// <summary>
        /// Converts a Base64 salt string to a byte array
        /// </summary>
        /// <param name="saltValue">The salt value to convert</param>
        public static byte[] ConvertSaltStringToBytes(string saltValue)
        {
            return Convert.FromBase64String(saltValue);
        }
    }
}
