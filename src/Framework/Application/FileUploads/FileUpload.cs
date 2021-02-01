using System;

namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Represents meta information to an file upload.
    /// </summary>
    public class FileUpload
    {
        /// <summary>
        /// Required by EF.
        /// </summary>
        private FileUpload()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUpload" /> class.
        /// </summary>
        /// <param name="id">Files identifier.</param>
        /// <param name="relativePath">Files relative paath from root folder.</param>
        /// <param name="extension">File extension.</param>
        /// <param name="contentType">Files content-type.</param>
        /// <param name="size">Size of the uploaded file.</param>
        /// <param name="expirationTime">When the files expires.</param>
        public FileUpload(Guid id, string relativePath, string extension, string contentType, long size, DateTime? expirationTime)
        {
            Id = id;
            UploadTime = DateTime.UtcNow;
            RelativeFileLocation = relativePath;
            Size = size;
            ContentType = contentType;
            Extension = extension;
            ExpirationTime = expirationTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUpload" /> class.
        /// </summary>
        /// <param name="relativePath">Files relative paath from root folder.</param>
        /// <param name="extension">File extension.</param>
        /// <param name="contentType">Files content-type.</param>
        /// <param name="size">Size of the uploaded file.</param>
        /// <param name="expirationTime">When the files expires.</param>
        public FileUpload(string relativePath, string extension, string contentType, long size, DateTime? expirationTime)
            : this(Guid.NewGuid(), relativePath, extension, contentType, size, expirationTime)
        {

        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the files content-type.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Gets the file extension without leading dot.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Gets the files relative stored loaction. (Relative from FileUploadSettings.RootPath)
        /// </summary>
        public string RelativeFileLocation { get; }

        /// <summary>
        /// Gets the file size.
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// Gets the expiration date of the file. If this property is null, the file is peristed.
        /// </summary>
        public DateTime? ExpirationTime { get; private set; }
        
        /// <summary>
        /// Gets the upload time.
        /// </summary>
        public DateTime UploadTime { get; }

        /// <summary>
        /// Persists the file to the storage.
        /// </summary>
        public void Persist()
        {
            ExpirationTime = null;
        }
    }
}
