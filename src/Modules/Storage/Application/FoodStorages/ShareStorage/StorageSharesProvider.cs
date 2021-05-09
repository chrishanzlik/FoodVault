using Dapper;
using FoodVault.Framework.Application.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage
{
    public static class StorageSharesProvider
    {
        public static async Task<IEnumerable<StorageShareDto>> GetSharesForStorageAsync(Guid storageId, IDbConnectionFactory dbConnectionFactory)
        {
            const string sql =
                "SELECT " +
                "[Share].[UserId]," +
                "[Share].[FoodStorageId], " +
                "[Share].[CanWrite] AS [WriteAccess] " +
                "FROM [storage].[StorageShares] AS [Share] " +
                "WHERE [Share].[FoodStorageId] = @storageId";

            var connection = dbConnectionFactory.GetOpen();

            var result = await connection.QueryAsync<StorageShareDto>(sql, new { storageId });

            return result.AsList();
        }
    }
}
