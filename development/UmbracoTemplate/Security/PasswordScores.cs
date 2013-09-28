namespace UmbracoTemplate.Security
{
    /// <summary>
    /// Defines the different password scores
    /// </summary>
    public enum PasswordScores
    {
        Blank = 0,
        TooShort = 1,
        RequirementsNotMet = 2,
        VeryWeak = 3,
        Weak = 4,
        Fair = 5,
        Medium = 6,
        Strong = 7,
        VeryStrong = 8
    }
}
