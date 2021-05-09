using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    /// <summary>
    /// Checks if a food storage name is forgiven.
    /// </summary>
    public class StorageNameUniquessSqlChecker : IStorageNameUniquessChecker
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageNameUniquessSqlChecker" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        public StorageNameUniquessSqlChecker(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public bool IsNameUniqueForUser(string storageName, Guid userId)
        {
            const string sql =
                "SELECT COUNT(1) " +
                "FROM [storage].[FoodStorages] " +
                "WHERE [FoodStorages].[Name] = @storageName AND [FoodStorages].[OwnerId] = @userId";

            var connection = _dbConnectionFactory.GetOpen();

            return !connection.ExecuteScalar<bool>(sql, new { storageName, userId });
        }
    }
}
