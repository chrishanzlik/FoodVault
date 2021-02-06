using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.GetProductImage
{
    /// <summary>
    /// Query handler for the <see cref="GetProductImageQuery"/>.
    /// </summary>
    internal class GetProductImageQueryHandler : IQueryHandler<GetProductImageQuery, FileUploadStream>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IFileStorage _fileStorage;
        private readonly IFileNameSanitizer _fileNameSanitizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductImageQueryHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        /// <param name="fileStorage">Applications file storage.</param>
        /// <param name="fileNameSanitizer">Download file name sanitizer.</param>
        public GetProductImageQueryHandler(
            IDbConnectionFactory dbConnectionFactory,
            IFileStorage fileStorage,
            IFileNameSanitizer fileNameSanitizer)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _fileStorage = fileStorage;
            _fileNameSanitizer = fileNameSanitizer;
        }

        /// <inheritdoc />
        public async Task<FileUploadStream> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT" +
                "[Product].[Name], " +
                "[Product].[ImageId] " +
                "FROM [storage].[Products] AS [Product] " +
                "WHERE [Product].[Id] = @productId";

            var connection = _dbConnectionFactory.GetOpen();

            var queryResult = await connection.QueryFirstOrDefaultAsync<(string Name, Guid ImageId)>(sql, new { productId = request.ProductId });
            if (queryResult.Name == null || queryResult.ImageId == Guid.Empty)
            {
                return null;
            }

            string sanitizedName = _fileNameSanitizer.Sanitize(queryResult.Name);

            return await _fileStorage.GetFileAsync(queryResult.ImageId, sanitizedName);
        }
    }
}
