using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.DataAccess;
using System;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages
{
    /// <summary>
    /// Checks access rights of users and storages
    /// </summary>
    public class StoragePermissionChecker : IStoragePermissionChecker
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoragePermissionChecker" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="executionContextAccessor"></param>
        public StoragePermissionChecker(IDbConnectionFactory dbConnectionFactory, IExecutionContextAccessor executionContextAccessor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        /// <inheritdoc />
        public async Task<bool> UserHasReadAccessAsync(Guid storageId)
        {
            const string sql =
                "SELECT " +
                "COUNT(1) " +
                "FROM [storage].[FoodStorages] AS [FoodStorage] " +
                "LEFT JOIN [storage].[StorageShares] as [StorageShare] " +
                "ON [StorageShare].[FoodStorageId] = [FoodStorage].[Id] AND [StorageShare].[UserId] = @userId " +
                "WHERE ([FoodStorage].[OwnerId] = @userId AND [FoodStorage].[Id] = @storageId) " +
                "OR [StorageShare].[Id] IS NOT NULL";

            if (!_executionContextAccessor.IsAvailable)
            {
                return false;
            }

            var connection = _dbConnectionFactory.GetOpen();

            return await connection.ExecuteScalarAsync<bool>(sql, new { storageId, userId = _executionContextAccessor.UserId });
        }

        /// <inheritdoc />
        public async Task<bool> UserHasWriteAccessAsync(Guid storageId)
        {
            const string sql =
                "SELECT " +
                "COUNT(1) " +
                "FROM [storage].[FoodStorages] AS [FoodStorage] " +
                "LEFT JOIN [storage].[StorageShares] as [StorageShare] " +
                "ON [StorageShare].[FoodStorageId] = [FoodStorage].[Id] AND [StorageShare].[UserId] = @userId " +
                "WHERE ([FoodStorage].[OwnerId] = @userId AND [FoodStorage].[Id] = @storageId) " +
                "OR ([StorageShare].[Id] IS NOT NULL AND [StorageShare].[CanWrite] = 1)";

            if (!_executionContextAccessor.IsAvailable)
            {
                return false;
            }

            var connection = _dbConnectionFactory.GetOpen();

            return await connection.ExecuteScalarAsync<bool>(sql, new { storageId, userId = _executionContextAccessor.UserId });
        }
    }
}
