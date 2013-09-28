namespace UmbracoTemplate.Security
{
    /// <summary>
    /// Defines a password service for generating passwords
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Checks the strength of a password
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <returns>The strength of the tested password</returns>
        PasswordScores CheckPasswordStrength(string password);
    }
}