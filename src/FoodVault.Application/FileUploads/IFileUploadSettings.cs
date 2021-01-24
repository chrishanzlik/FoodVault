using System.Collections.Generic;

namespace FoodVault.Application.FileUploads
{
    /// <summary>
    /// Settings interface for <see cref="FileUpload"/> handling.
    /// </summary>
    public interface IFileUploadSettings
    {
        /// <summary>
        /// Gets the root folder of all uploads.
        /// </summary>
        public string RootFolder { get; }

        /// <summary>
        /// Gets a collection of allowed file extensions.
        /// </summary>
        public IEnumerable<string> AllowedExtensions { get; }

        /// <summary>
        /// Gets the maximum allowed upload file size in MegaByte.
        /// </summary>
        public double MaximumFileSize { get; }
    }
}
