using System.Linq;
using System.Text.RegularExpressions;

namespace UmbracoTemplate.Security
{
    public class PasswordService : IPasswordService
    {
        public PasswordScores CheckPasswordStrength(string password)
        {
            var score = 0;

            // using three requirements here:  min length and two types of characters (numbers and letters)
            var requirement1Met = false;
            var requirement2Met = false;

            // check for chars in password
            if (password.Length < 1)
                return PasswordScores.Blank;

            // if less than 6 chars, return as too short, else, plus one
            if (password.Length < 6)
            {
                return PasswordScores.TooShort;
            }

            score++;

            // if 8 or more chars, plus one
            if (password.Length >= 8)
                score++;

            // if 10 or more chars, plus one
            if (password.Length >= 10)
                score++;

            // if password has a number, plus one
            if (Regex.IsMatch(password, @"[\d]", RegexOptions.ECMAScript))
            {
                score++;
                requirement1Met = true;
            }

            // if password has lower case letter, plus one
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript))
            {
                score++;
                requirement2Met = true;
            }

            // if password has upper case letter, plus one
            if (Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
            {
                score++;
                requirement2Met = true;
            }

            // if password has a special character, plus one
            if (Regex.IsMatch(password, @"[~`!@#$%\^\&\*\(\)\-_\+=\[\{\]\}\|\\;:'\""<\,>\.\?\/£]", RegexOptions.ECMAScript))
                score++;

            // if password is longer than 2 characters and has 3 repeating characters, minus one (to minimum of score of 3)
            var paswwordChars = password.ToList();
            if (paswwordChars.Count >= 3)
            {
                for (var i = 2; i < paswwordChars.Count; i++)
                {
                    var charCurrent = paswwordChars[i];
                    if (charCurrent == paswwordChars[i - 1] && charCurrent == paswwordChars[i - 2] && score >= 4)
                    {
                        score++;
                    }
                }
            }

            if (!requirement1Met || !requirement2Met)
            {
                return PasswordScores.RequirementsNotMet;
            }

            return (PasswordScores)score;
        }
    }
}
