using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Modules.Storage.Application.FoodStorages.DomainServices
{
    /// <summary>
    /// Service that provides user id's for a shared storage.
    /// </summary>
    public class StorageUserSharesFinder : IStorageUserSharesFinder
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageUserSharesFinder" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        public StorageUserSharesFinder(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public IEnumerable<UserId> GetSharedUserIdsForStorage(FoodStorageId foodStorageId)
        {
            const string sql =
                "SELECT " +
                "[Share].[UserId] " +
                "FROM [storage].[StorageShares] AS [Share] " +
                "WHERE [Share].[FoodStorageId] = @foodStorageId";

            var connection = _dbConnectionFactory.GetOpen();

            var result = connection.Query<Guid>(sql, new { foodStorageId = foodStorageId.Value }).AsList();

            return result.Select(x => new UserId(x)).ToList();
        }
    }
}
