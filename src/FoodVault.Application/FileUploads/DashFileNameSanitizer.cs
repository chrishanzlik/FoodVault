using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FoodVault.Application.FileUploads
{
    /// <summary>
    /// Replaces invalid and disallowed characters with dashes. Ensures a pretty output name.
    /// </summary>
    public class DashFileNameSanitizer : IFileNameSanitizer
    {
        private static readonly string _invalidCharacters = new string(Path.GetInvalidFileNameChars());
        private static readonly string _disallowedCharacters = " !$&§%(){}=#+'~;";

        private static readonly Regex _uglyDotRegex = new Regex(@"(-\.)|(\.-)|(-\.-)", RegexOptions.Compiled);
        private static readonly Regex _multiDotRegex = new Regex(@"(\.{2,})", RegexOptions.Compiled);
        private static readonly Regex _charReplaceRegex = new Regex(
            string.Format(@"([{0}{1}]*\.+$)|([{0}{1}]+)", _invalidCharacters, _disallowedCharacters),
            RegexOptions.Compiled);

        /// <inheritdoc />
        public string Sanitize(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Parameter 'fileName' cannot be null or empty.");
            }

            fileName = _charReplaceRegex.Replace(fileName, "-");
            fileName = _uglyDotRegex.Replace(fileName, ".");
            fileName = _multiDotRegex.Replace(fileName, ".");

            return fileName.StartsWith('-') ? fileName[1..] : fileName;
        }
    }
}
