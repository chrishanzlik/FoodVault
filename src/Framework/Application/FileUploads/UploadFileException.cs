using System;

namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Exception that occurs while a file upload process.
    /// </summary>
    public class UploadFileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileException" /> class.
        /// </summary>
        /// <param name="message"></param>
        public UploadFileException(string message) : base(message) 
        {
        }
    }
}
