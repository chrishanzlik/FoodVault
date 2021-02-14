using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using FoodVault.Modules.Storage.Application.Common;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageContentQueryHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="urlBuilder"></param>
        public GetStorageContentQueryHandler(
            IDbConnectionFactory dbConnectionFactory,
            IStorageModuleUrlBuilder urlBuilder)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _urlBuilder = urlBuilder;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StoredProductDto>> Handle(GetStorageContentQuery request, CancellationToken cancellationToken)
        {
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
