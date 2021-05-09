using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesForUser
{
    /// <summary>
    /// Query handler for <see cref="GetStoragesForUserQuery"/>.
    /// </summary>
    internal class GetStoragesForUserQueryHandler : IQueryHandler<GetStoragesForUserQuery, IEnumerable<FoodStorageDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStoragesForUserQueryHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection.</param>
        /// <param name="dbConnectionFactory">Execution context accessor.</param>
        public GetStoragesForUserQueryHandler(IDbConnectionFactory dbConnectionFactory, IExecutionContextAccessor executionContextAccessor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FoodStorageDto>> Handle(GetStoragesForUserQuery request, CancellationToken cancellationToken)
        {
            if (!_executionContextAccessor.IsAvailable)
            {
                return null;
            }

            Guid userId = _executionContextAccessor.UserId;

            //TODO: Include storage shares

            const string sql =
                "SELECT " +
                "[Storage].[Id]," +
                "[Storage].[Name]," +
                "[Storage].[Description], " +
                "SUM([StoredProduct].[Quantity]) AS [Products], " +
                "SUM(CASE WHEN [StoredProduct].[ExpirationDate] < GETDATE() THEN [StoredProduct].[Quantity] ELSE NULL END) AS [ExpiredProducts] " +
                "FROM [storage].[FoodStorages] as [Storage] " +
                "LEFT JOIN [storage].[StoredProducts] AS [StoredProduct] ON [StoredProduct].[FoodStorageId] = [Storage].[Id] " +
                "WHERE [Storage].[IsDeleted] = 0 AND [Storage].[OwnerId] = @ownerId AND (@nameFilter IS NULL OR [Storage].[Name] LIKE @nameFilter) " +
                "GROUP BY " +
                "[Storage].[Id]," +
                "[Storage].[Name]," +
                "[Storage].[Description]";

            var con = _dbConnectionFactory.GetOpen();

            var storages = await con.QueryAsync<FoodStorageDto>(sql, new {
                ownerId = userId,
                nameFilter = '%' + request.NameFilter + '%' });

            return storages.AsList();
        }
    }
}
