using System;
using System.IO;
using System.Security.Cryptography;

namespace UmbracoTemplate.Security
{
    /// <summary>
    /// Immutable encryption class providing encryption and decryption methods
    /// </summary>
    internal class ImmutableCryptographer : IDisposable
    {
        private static readonly byte[] DefaultSalt =
            {
                0x19, 0x90, 0x56, 0x1e, 0x20, 0x4d, 0x65, 0x68, 0x72, 0x65, 0x61,
                0x38, 0x76
            };

        private byte[] key;
        private byte[] iv;
        private SymmetricAlgorithm encryptionAlgorithm;

        public ImmutableCryptographer(string password, byte[] salt = null)
            : this(EncryptionTypes.AES, password, salt)
        {
        }

        public ImmutableCryptographer(EncryptionTypes type, string password, byte[] salt = null)
        {
            EncryptionType = type;
            Password = password;
            Salt = salt ?? DefaultSalt;

            CalculateKeyAndVector();
        }
        
        /// <summary>
        /// Type of encryption / decryption used
        /// </summary>
        public EncryptionTypes EncryptionType { get; private set; }

        /// <summary>
        ///	Passsword Key Property.
        /// The password key used when encrypting / decrypting
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the currently used Salt value
        /// </summary>
        public byte[] Salt { get; private set; }

        /// <summary>
        /// Performs the actual encoded/decoded bytes
        /// </summary>
        /// <param name="inputBytes">Input byte array</param>
        /// <param name="direction">Whether to perform encoding or decoding</param>
        /// <returns>Byte array output</returns>
        public byte[] Transform(byte[] inputBytes, CryptographyTransformDirection direction)
        {
            var transform = GetEncryptionTransform(direction);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }


        /// <summary>
        ///	Returns the symmetric engine and creates the encyptor/decryptor
        /// </summary>
        /// <param name="direction">Whether to return a encrpytor or decryptor</param>
        private ICryptoTransform GetEncryptionTransform(CryptographyTransformDirection direction)
        {
            return direction == CryptographyTransformDirection.Encrypt ? 
                    GetEncryptionAlgorithm().CreateEncryptor(key, iv) : 
                    GetEncryptionAlgorithm().CreateDecryptor(key, iv);
        }

        /// <summary>
        ///	Returns the specific symmetric algorithm according to the cryptotype
        /// </summary>
        private SymmetricAlgorithm GetEncryptionAlgorithm()
        {
            return encryptionAlgorithm ?? (encryptionAlgorithm = GetEncryptionAlgorithm(EncryptionType));
        }

        /// <summary>
        ///	Calculates the key and IV according to the symmetric method from the password key and IV size dependant on symmetric method
        /// </summary>
        private void CalculateKeyAndVector()
        {
            var derivedBytes = new Rfc2898DeriveBytes(Password, Salt);
            var algorithm = GetEncryptionAlgorithm();

            key = derivedBytes.GetBytes(algorithm.KeySize / 8);
            iv = derivedBytes.GetBytes(algorithm.BlockSize / 8);
        }

        /// <summary>
        ///	Returns the specific symmetric algorithm based on the <c>EncryptionTypes</c> input
        /// </summary>
        private static SymmetricAlgorithm GetEncryptionAlgorithm(EncryptionTypes encryptionTypes)
        {
            switch (encryptionTypes)
            {
                case EncryptionTypes.DES:
                    return DES.Create();
                case EncryptionTypes.RC2:
                    return RC2.Create();
                case EncryptionTypes.Rijndael:
                    return Rijndael.Create();
                case EncryptionTypes.TripleDES:
                    return TripleDES.Create();
                case EncryptionTypes.AES:
                    return Aes.Create();
                default:
                    return Aes.Create();
            }
        }

        public void Dispose()
        {
            if (encryptionAlgorithm != null)
                encryptionAlgorithm.Dispose();
        }
    }
}
