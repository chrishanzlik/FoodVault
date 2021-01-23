using FoodVault.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Application.FileUploads
{
    public class FileUpload
    {
        /// <summary>
        /// Required by EF.
        /// </summary>
        private FileUpload()
        {

        }

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

        public FileUpload(string relativePath, string extension, string contentType, long size, DateTime? expirationTime)
        {
            Id = Guid.NewGuid();
            UploadTime = DateTime.UtcNow;
            RelativeFileLocation = relativePath;
            Size = size;
            ContentType = contentType;
            Extension = extension;
            ExpirationTime = expirationTime;
        }

        public Guid Id { get; }
        public string ContentType { get; }

        /// <summary>
        /// w/o leading dot
        /// </summary>
        public string Extension { get; }
        public string RelativeFileLocation { get; }
        public long Size { get; }
        public DateTime? ExpirationTime { get; private set; }
        /// <summary>
        /// utc
        /// </summary>
        public DateTime UploadTime { get; }

        public void Persist()
        {
            ExpirationTime = null;
        }
    }
}
