using System;
using System.Text;

namespace UmbracoTemplate.Security
{
    public class CryptographyService : ICryptographyService
    {
        public byte[] Encrypt(string password, byte[] inputData, byte[] salt = null)
        {
            using (var cryptographer = new ImmutableCryptographer(password, salt))
            {
                return cryptographer.Transform(inputData, CryptographyTransformDirection.Encrypt);
            }
        }

        public string Encrypt(string password, string inputString, string salt = "")
        {
            using (var cryptographer = new ImmutableCryptographer(password, string.IsNullOrEmpty(salt) ? null : CryptographyUtils.ConvertSaltStringToBytes(salt)))
            {
                return Convert.ToBase64String(cryptographer.Transform(Encoding.UTF8.GetBytes(inputString), CryptographyTransformDirection.Encrypt));
            }
        }

        public byte[] Decrypt(string password, byte[] inputData, byte[] salt = null)
        {
            using (var cryptographer = new ImmutableCryptographer(password, salt))
            {
                return cryptographer.Transform(inputData, CryptographyTransformDirection.Decrypt);
            }
        }

        public string Decrypt(string password, string inputString, string salt = "")
        {
            using (var cryptographer = new ImmutableCryptographer(password, string.IsNullOrEmpty(salt) ? null : CryptographyUtils.ConvertSaltStringToBytes(salt)))
            {
                return Encoding.UTF8.GetString(cryptographer.Transform(Convert.FromBase64String(inputString), CryptographyTransformDirection.Decrypt));
            }
        }

        public byte[] SecureHash(byte[] inputData, byte[] salt)
        {
            var hasher = new ImmutableHasher(salt);
            return hasher.SecureHash(inputData);
        }

        public string SecureHash(string inputString, string salt)
        {
            var hasher = new ImmutableHasher(CryptographyUtils.ConvertSaltStringToBytes(salt));
            return Convert.ToBase64String(hasher.SecureHash(Encoding.UTF8.GetBytes(inputString)));
        }

        public byte[] Hash(byte[] inputData)
        {
            var hasher = new ImmutableHasher();
            return hasher.Hash(inputData);
        }

        public string Hash(string inputString)
        {
            var hasher = new ImmutableHasher();
            return Convert.ToBase64String(hasher.Hash(Encoding.UTF8.GetBytes(inputString)));
        }

        public byte[] GenerateSaltBytes(string password)
        {
            return CryptographyUtils.GenerateSaltBytesFromString(password);
        }

        public string GenerateSaltString(string password)
        {
            return CryptographyUtils.GenerateSaltStringFromString(password);
        }
    }
}
