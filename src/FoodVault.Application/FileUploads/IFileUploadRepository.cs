using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodVault.Application.FileUploads
{
    /// <summary>
    /// Interface of a repository for interacting with <see cref="FileUpload"/>s.
    /// </summary>
    public interface IFileUploadRepository
    {
        /// <summary>
        /// Searches for a <see cref="FileUpload"/> by its id.
        /// </summary>
        /// <param name="id">Id of the <see cref="FileUpload"/> to find.</param>
        /// <returns>The matching <see cref="FileUpload"/> or null if not found.</returns>
        Task<FileUpload> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all expired <see cref="FileUpload"/>s.
        /// </summary>
        /// <returns>An enumerable of expired <see cref="FileUpload"/>s.</returns>
        Task<IEnumerable<FileUpload>> GetExpiredFilesAsync();

        /// <summary>
        /// Removes a <see cref="FileUpload"/> with the given id from the repository.
        /// </summary>
        /// <param name="id">Id of the upload to remove.</param>
        /// <returns>Awaitable task.</returns>
        Task RemoveAsync(Guid id);

        /// <summary>
        /// Adds a <see cref="FileUpload"/> to the repository.
        /// </summary>
        /// <param name="fileUpload">The <see cref="FileUpload"/> which should be added to the repository.</param>
        /// <returns>Awaitable task.</returns>
        Task AddAsync(FileUpload fileUpload);

        /// <summary>
        /// Persists a temporary uploaded <see cref="FileUpload"/>.
        /// </summary>
        /// <param name="id">Id of the <see cref="FileUpload"/> to perists.</param>
        /// <returns>Awaitable task.</returns>
        Task PersistFileAsync(Guid id);
    }
}
