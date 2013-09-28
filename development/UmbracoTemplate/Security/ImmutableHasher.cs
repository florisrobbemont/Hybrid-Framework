using System;
using System.Security.Cryptography;

namespace UmbracoTemplate.Security
{
    /// <summary>
    /// Immutable encryption class providing hashing methods
    /// </summary>
    internal class ImmutableHasher
    {
        public ImmutableHasher()
        {
        }

        public ImmutableHasher(byte[] salt)
        {
            Salt = salt;
        }

        /// <summary>
        /// Gets the currently used Salt value
        /// </summary>
        public byte[] Salt { get; private set; }

        /// <summary>
        /// Generates a secure hash
        /// </summary>
        /// <param name="inputBytes">Input byte array</param>
        /// <returns>Byte array output</returns>
        public byte[] SecureHash(byte[] inputBytes)
        {
            if(Salt == null)
                throw new InvalidOperationException("Cannot generate secure hash without a salt value!");

            using (var hasher = new Rfc2898DeriveBytes(inputBytes, Salt, 1000))
            {
                return hasher.GetBytes(32);
            }
        }

        /// <summary>
        /// Generates a unique hash weak for high-performance hashing 
        /// </summary>
        /// <param name="inputBytes">Input byte array</param>
        /// <returns>Byte array output</returns>
        public byte[] Hash(byte[] inputBytes)
        {
            using (var hasher = new MD5CryptoServiceProvider())
            {
                return hasher.ComputeHash(inputBytes);
            }
        }
    }
}
