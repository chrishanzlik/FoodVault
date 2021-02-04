using Dapper;
using FoodVault.Framework.Application.Database;
using FoodVault.Framework.Application.FileUploads;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.FileUploads
{
    /// <summary>
    /// SQL repository for <see cref="FileUpload"/>s.
    /// </summary>
    public class FileUploadSqlRepository : IFileUploadRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadSqlRepository" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">SQL db connection factory.</param>
        public FileUploadSqlRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public Task AddAsync(FileUpload fileUpload)
        {
            const string insertSql =
                "INSERT INTO [storage].[FileUploads](Id, ContentType, Extension, RelativeFileLocation, Size, ExpirationTime, UploadTime) " +
                "VALUES (@Id, @ContentType, @Extension, @RelativeFileLocation, @Size, @ExpirationTime, @UploadTime)";

            var connection = _dbConnectionFactory.GetOpen();

            return connection.ExecuteAsync(insertSql, fileUpload);
        }

        /// <inheritdoc />
        public Task<FileUpload> GetByIdAsync(Guid id)
        {
            const string querySql = "SELECT * FROM [storage].[FileUploads] WHERE [Id] = @id";

            var connection = _dbConnectionFactory.GetOpen();

            return connection.QueryFirstOrDefaultAsync<FileUpload>(querySql, new { id });
        }

        /// <inheritdoc />
        public Task<IEnumerable<FileUpload>> GetExpiredFilesAsync()
        {
            const string querySql =
                "SELECT * FROM [storage].[FileUploads] WHERE [ExpirationTime] IS NOT NULL AND [ExpirationTime] < @now";

            var connection = _dbConnectionFactory.GetOpen();

            return connection.QueryAsync<FileUpload>(querySql, new { now = DateTime.UtcNow });
        }

        /// <inheritdoc />
        public Task PersistFileAsync(Guid id)
        {
            const string persistSql = "UPDATE [storage].[FileUploads] SET [ExpirationTime] = NULL WHERE [Id] = @id";

            var connection = _dbConnectionFactory.GetOpen();

            return connection.ExecuteAsync(persistSql, new { id });
        }

        /// <inheritdoc />
        public Task RemoveAsync(Guid id)
        {
            const string removeSql ="DELETE FROM [storage].[FileUploads] WHERE [Id] = @id";

            var connection = _dbConnectionFactory.GetOpen();

            return connection.ExecuteAsync(removeSql, new { id });
        }
    }
}
