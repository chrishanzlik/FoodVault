using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodVault.Application.FileUploads
{
    /// <summary>
    /// Saving or loading files.
    /// </summary>
    public interface IFileStorage
    {
        Task<FileUploadStream> GetFileAsync(Guid id, string newFileName = null);
        Task<Guid> StoreFileTemporaryAsync(Stream fileStream, string fileName, string contentType, TimeSpan expirationTime);
        Task PersistFileAsync(Guid id);
        Task DeleteFileAsync(Guid id);
        Task DeleteExpiredFilesAsync();
    }
}
