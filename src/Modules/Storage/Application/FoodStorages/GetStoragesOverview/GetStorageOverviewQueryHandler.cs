using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesOverview
{
    /// <summary>
    /// Query handler for <see cref="GetStorageOverviewQuery"/>.
    /// </summary>
    internal class GetStorageOverviewQueryHandler : IQueryHandler<GetStorageOverviewQuery, IEnumerable<FoodStorageDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageOverviewQueryHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection.</param>
        public GetStorageOverviewQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FoodStorageDto>> Handle(GetStorageOverviewQuery request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT " +
                "[Storage].[Id]," +
                "[Storage].[Name]," +
                "[Storage].[Description], " +
                "SUM([StoredProduct].[Quantity]) AS [Products], " +
                "SUM(CASE WHEN [StoredProduct].[ExpirationDate] < GETDATE() THEN [StoredProduct].[Quantity] ELSE NULL END) AS [ExpiredProducts] " +
                "FROM [storage].[FoodStorages] as [Storage] " +
                "LEFT JOIN [storage].[StoredProducts] AS [StoredProduct] ON [StoredProduct].[FoodStorageId] = [Storage].[Id] " +
                "WHERE [Storage].[IsDeleted] = 0 " +
                "GROUP BY " +
                "[Storage].[Id]," +
                "[Storage].[Name]," +
                "[Storage].[Description]";

            var con = _dbConnectionFactory.GetOpen();

            var storages = await con.QueryAsync<FoodStorageDto>(sql);

            return storages.AsList();
        }
    }
}
