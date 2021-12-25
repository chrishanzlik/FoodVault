using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using FoodVault.Modules.Storage.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageContent
{
    /// <summary>
    /// Query handler for the <see cref="GetStorageContentQuery"/>.
    /// </summary>
    public class GetStorageContentQueryHandler : IQueryHandler<GetStorageContentQuery, IEnumerable<StoredProductDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IStorageModuleUrlBuilder _urlBuilder;
        private readonly IStoragePermissionChecker _storagePermissionChecker;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageContentQueryHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        /// <param name="urlBuilder">Url builder.</param>
        /// <param name="storagePermissionChecker">Storage permission checker.</param>
        public GetStorageContentQueryHandler(
            IDbConnectionFactory dbConnectionFactory,
            IStorageModuleUrlBuilder urlBuilder,
            IStoragePermissionChecker storagePermissionChecker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _urlBuilder = urlBuilder;
            _storagePermissionChecker = storagePermissionChecker;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StoredProductDto>> Handle(GetStorageContentQuery request, CancellationToken cancellationToken)
        {
            if (!await _storagePermissionChecker.UserHasReadAccessAsync(request.FoodStorageId))
            {
                throw new ApplicationException("User is not allowed to read from this storage.");
            }

            const string sql =
                "SELECT " +
                "[Product].[Id] AS [ProductId]," +
                "[Product].[Name]," +
                "[Product].[Brand]," +
                "[Product].[Barcode]," +
                "CONVERT(nvarchar(36), [Product].[ImageId]) AS [ImageUrl]," +
                "[StoredProduct].[Quantity]," +
                "[StoredProduct].[ExpirationDate] " +
                "FROM [storage].[StoredProducts] AS [StoredProduct] " +
                "JOIN [storage].[Products] AS [Product] ON [StoredProduct].[ProductId] = [Product].[Id] " +
                "WHERE [StoredProduct].[FoodStorageId] = @storageId";

            var connection = _dbConnectionFactory.GetOpen();
            var result = (await connection.QueryAsync<StoredProductDto>(sql, new { storageId = request.FoodStorageId })).AsList();

            foreach(var product in result.Where(x => !string.IsNullOrEmpty(x.ImageUrl)))
            {
                product.ImageUrl = _urlBuilder.BuildProductImageUrl(product.ProductId);
            }

            return result;
        }
    }
}
