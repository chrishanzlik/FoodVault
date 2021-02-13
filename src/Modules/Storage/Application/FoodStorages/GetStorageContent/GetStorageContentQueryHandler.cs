using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageContent
{
    public class GetStorageContentQueryHandler : IQueryHandler<GetStorageContentQuery, IEnumerable<StoredProductDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetStorageContentQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<StoredProductDto>> Handle(GetStorageContentQuery request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT " +
                "[Product].[Id] AS [ProductId]," +
                "[Product].[Name]," +
                "[Product].[Brand]," +
                "[Product].[Barcode]," +
                "[StoredProduct].[Quantity]," +
                "[StoredProduct].[ExpirationDate] " +
                "FROM [storage].[StoredProducts] AS [StoredProduct] " +
                "JOIN [storage].[Products] AS [Product] ON [StoredProduct].[ProductId] = [Product].[Id] " +
                "WHERE [StoredProduct].[FoodStorageId] = @storageId";

            var connection = _dbConnectionFactory.GetOpen();
            var result = await connection.QueryAsync<StoredProductDto>(sql, new { storageId = request.FoodStorageId });

            return result.AsList();
        }
    }
}
