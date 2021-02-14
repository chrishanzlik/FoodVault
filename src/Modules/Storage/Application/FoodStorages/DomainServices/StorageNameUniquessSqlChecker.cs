using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.Storage.Domain.FoodStorages;

namespace FoodVault.Modules.Storage.Application.FoodStorages.DomainServices
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
        public bool StorageNameIsUnique(string storageName)
        {
            //TODO: Scope to user

            const string sql =
                "SELECT COUNT(1) FROM [storage].[FoodStorages] WHERE [FoodStorages].[Name] = @storageName";

            var connection = _dbConnectionFactory.GetOpen();

            return !connection.ExecuteScalar<bool>(sql, new { storageName });
        }
    }
}
