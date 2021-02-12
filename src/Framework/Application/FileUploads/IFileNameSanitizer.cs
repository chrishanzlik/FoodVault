namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Class for sanitizing file names.
    /// </summary>
    public interface IFileNameSanitizer
    {
        /// <summary>
        /// Gets rid of invalid or unallowed characters within a file name.
        /// </summary>
        /// <param name="fileName">Name to sanitize.</param>
        /// <param name="extension">Optional extension to append. Specify without leading dot!</param>
        /// <returns>Sanitized file name.</returns>
        string Sanitize(string fileName, string extension);
    }
}
