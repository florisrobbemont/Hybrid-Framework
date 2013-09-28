namespace UmbracoTemplate.Security
{
    public interface ICryptographyService
    {
        /// <summary>
        /// Encrypts a byte array
        /// </summary>
        /// <param name="password">The password used for the encryption</param>
        /// <param name="inputData">Byte array to encrypt</param>
        /// <param name="salt">Optional Salt value for encryption</param>
        /// <returns>an encrypted byte array</returns>
        byte[] Encrypt(string password, byte[] inputData, byte[] salt = null);

        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="password">The password used for the encryption</param>
        /// <param name="inputString">Text to encrypt</param>
        /// <param name="salt">Optional Salt value for encryption</param>
        /// <returns>an encrypted (Base64) string</returns>
        string Encrypt(string password, string inputString, string salt = "");

        /// <summary>
        /// Decrypts a byte array
        /// </summary>
        /// <param name="password">The password used for the encryption</param>
        /// <param name="inputData">Byte array to encrypt</param>
        /// <param name="salt">Optional Salt value for encryption</param>
        /// <returns>an decrypted byte array</returns>
        byte[] Decrypt(string password, byte[] inputData, byte[] salt = null);

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="password">The password used for the encryption</param>
        /// <param name="inputString">Base64 text to decrypt</param>
        /// <param name="salt">Optional Salt value for decryption</param>
        /// <returns>an decrypted (UTF8) string</returns>
        string Decrypt(string password, string inputString, string salt = "");

        /// <summary>
        /// Generates a hash based on a password and salt
        /// </summary>
        /// <param name="inputData">Data to hash</param>
        /// <param name="salt">Salt value for the hash</param>
        /// <returns>a hash byte array representing the input data</returns>
        byte[] SecureHash(byte[] inputData, byte[] salt);

        /// <summary>
        /// Generates a hash based on a password and salt
        /// </summary>
        /// <param name="password">Password for the hash</param>
        /// <param name="inputString">String to hash</param>
        /// <param name="salt">Salt value for the hash</param>
        /// <returns>a hash string representing the input data</returns>
        string SecureHash(string inputString, string salt);

        /// <summary>
        /// Generates a hash
        /// </summary>
        /// <param name="inputData">Data to hash</param>
        /// <returns>a hash byte array representing the input data</returns>
        byte[] Hash(byte[] inputData);

        /// <summary>
        /// Generates a hash
        /// </summary>
        /// <param name="inputString">String to hash</param>
        /// <returns>a hash string representing the input data</returns>
        string Hash(string inputString);

        /// <summary>
        /// Generates a unique salt value from a password
        /// </summary>
        /// <param name="password">The password used to generate the salt value</param>
        /// <returns>A byte array containing the unique salt value</returns>
        byte[] GenerateSaltBytes(string password);

        /// <summary>
        /// Generates a unique salt value from a password
        /// </summary>
        /// <param name="password">The password used to generate the salt value</param>
        /// <returns>A string containing the unique salt value</returns>
        string GenerateSaltString(string password);
    }
}