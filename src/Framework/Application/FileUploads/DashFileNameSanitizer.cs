using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Replaces invalid and disallowed characters with dashes. Ensures a pretty output name.
    /// </summary>
    public class DashFileNameSanitizer : IFileNameSanitizer
    {
        private static readonly string _invalidCharacters = new(Path.GetInvalidFileNameChars());
        private static readonly string _disallowedCharacters = ". !$&§%(){}=#+'~;";

        private static readonly Regex _charReplaceRegex = new(
            string.Format(@"([{0}{1}]*\.+$)|([{0}{1}]+)", _invalidCharacters, _disallowedCharacters),
            RegexOptions.Compiled);

        /// <inheritdoc />
        public string Sanitize(string fileName, string extension)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Parameter 'fileName' cannot be null or empty.");
            }

            fileName = _charReplaceRegex.Replace(fileName, "-").Trim('-');

            if (!string.IsNullOrWhiteSpace(extension))
            {
                if (extension.StartsWith('.'))
                {
                    extension = extension[1..];
                }

                fileName += $".{extension}";
            }

            return fileName; ;
        }
    }
}
