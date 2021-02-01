using System;

namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Identifier for a file upload.
    /// </summary>
    public sealed class FileUploadId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadId" /> class.
        /// </summary>
        /// <param name="value">Identifiers value.</param>
        public FileUploadId(Guid value) : base(value)
        {
        }
    }
}
