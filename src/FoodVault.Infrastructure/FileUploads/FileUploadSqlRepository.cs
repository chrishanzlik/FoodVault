using FoodVault.Application.Database;
using FoodVault.Application.FileUploads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.FileUploads
{
    /// <summary>
    /// SQL repository for <see cref="FileUpload"/>s.
    /// </summary>
    public class FileUploadSqlRepository : IFileUploadRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public FileUploadSqlRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public Task AddAsync(FileUpload fileUpload)
        {
            var connection = _dbConnectionFactory.GetOpen();

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<FileUpload> GetByIdAsync(Guid id)
        {
            var connection = _dbConnectionFactory.GetOpen();

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<FileUpload>> GetExpiredFilesAsync()
        {
            var connection = _dbConnectionFactory.GetOpen();

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task PersistFileAsync(Guid id)
        {
            var connection = _dbConnectionFactory.GetOpen();

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task RemoveAsync(Guid id)
        {
            var connection = _dbConnectionFactory.GetOpen();

            throw new NotImplementedException();
        }
    }
}
