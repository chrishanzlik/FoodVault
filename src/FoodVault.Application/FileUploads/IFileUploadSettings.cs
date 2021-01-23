namespace FoodVault.Application.FileUploads
{
    /// <summary>
    /// Settings interface for <see cref="FileUpload"/> handling.
    /// </summary>
    public interface IFileUploadSettings
    {
        /// <summary>
        /// Gets or sets the root folder of all uploads.
        /// </summary>
        public string RootFolder { get; set; }
    }
}
