using System.Collections.Generic;

namespace FoodVault.Application.FileUploads
{
    /// <summary>
    /// File upload settings.
    /// </summary>
    public class FileUploadSettings : IFileUploadSettings
    {
        /// <inheritdoc />
        public string RootFolder { get; set; }

        /// <inheritdoc />
        public IEnumerable<string> AllowedExtensions { get; set; }

        /// <inheritdoc />
        public double MaximumFileSize { get; set; }
    }
}
