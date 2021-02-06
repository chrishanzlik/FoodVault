using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Interface for a storage that persists and loads files.
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        /// Gets a file by its identifier.
        /// </summary>
        /// <param name="id">Id of the file to load.</param>
        /// <param name="newFileName">How the file should be named.</param>
        /// <returns>File wrapped inside a <see cref="FileUploadStream"/>.</returns>
        Task<FileUploadStream> GetFileAsync(Guid id, string newFileName = null);

        /// <summary>
        /// Stores a file temporary. After expiration the file is marked for cleanup.
        /// </summary>
        /// <param name="fileStream">Filestream that contains the file content.</param>
        /// <param name="fileName">Original name of the file.</param>
        /// <param name="contentType">Files content type.</param>
        /// <param name="expirationTime">When the uploaded file expires.</param>
        /// <returns>Id of the temporary stored file.</returns>
        Task<Guid> StoreFileTemporaryAsync(Stream fileStream, string fileName, string contentType, TimeSpan expirationTime);

        /// <summary>
        /// Persists a file to the file storage by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the file to perist.</param>
        /// <returns>Awaitable task.</returns>
        Task PersistFileAsync(Guid id);

        /// <summary>
        /// Deletes a file from the file storage.
        /// </summary>
        /// <param name="id">Identifier of the file which should be removed.</param>
        /// <returns>Awaitable task.</returns>
        Task DeleteFileAsync(Guid id);

        /// <summary>
        /// Removes all expired files from the file storage.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        Task DeleteExpiredFilesAsync();
    }
}
