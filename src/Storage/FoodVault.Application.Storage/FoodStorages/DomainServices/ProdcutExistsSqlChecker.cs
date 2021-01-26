using Dapper;
using FoodVault.Application.Database;
using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;

namespace FoodVault.Application.Storage.FoodStorages.DomainServices
{
    /// <summary>
    /// Checks if a product exists.
    /// </summary>
    public class ProdcutExistsSqlChecker : IProductExistsChecker
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdcutExistsSqlChecker" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        public ProdcutExistsSqlChecker(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public bool ProductExists(ProductId id)
        {
            const string sql = "SELECT COUNT(DISTINCT 1) FROM [dbo].[Products] WHERE Id = @id";

            var connection = _dbConnectionFactory.GetOpen();

            return connection.ExecuteScalar<bool>(sql, new { id = id.Value });
        }
    }
}
