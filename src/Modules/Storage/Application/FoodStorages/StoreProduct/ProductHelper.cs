using Dapper;
using FoodVault.Framework.Application.DataAccess;
using System;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.StoreProduct
{
    public static class ProductHelper
    {
        public static Task<bool> CheckProductExistenceAsync(Guid productId, IDbConnectionFactory dbConnectionFactory)
        {
            const string sql = "SELECT COUNT(DISTINCT 1) FROM [storage].[Products] WHERE Id = @productId";

            var connection = dbConnectionFactory.GetOpen();

            return connection.ExecuteScalarAsync<bool>(sql, new { productId });
        }
    }
}
