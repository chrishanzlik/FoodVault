using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using FoodVault.Modules.Storage.Domain.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageShares
{
    /// <summary>
    /// Query handler for the <see cref="GetStorageSharesQuery"/>.
    /// </summary>
    internal class GetStorageSharesQueryHandler : IQueryHandler<GetStorageSharesQuery, IEnumerable<StorageShareDto>>
    {
        private readonly IUserContext _userContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageSharesQueryHandler" /> class.
        /// </summary>
        /// <param name="userContext"></param>
        /// <param name="dbConnectionFactory"></param>
        public GetStorageSharesQueryHandler(IUserContext userContext, IDbConnectionFactory dbConnectionFactory)
        {
            _userContext = userContext;
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageShareDto>> Handle(GetStorageSharesQuery request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT " +
                "[Share].[UserId]," +
                "[Share].[SharedAt] " +
                "FROM [storage].[StorageShares] AS [Share] " +
                "JOIN [storage].[FoodStorages] AS [Storage] ON [Storage].[Id] = [Share].[FoodStorageId] " +
                "WHERE [Share].[FoodStorageId] = @foodStorageId AND [Storage].[OwnerId] = @ownerId";

            var connection = _dbConnectionFactory.GetOpen();
            var result = await connection.QueryAsync<StorageShareDto>(sql, new
            {
                ownerId = _userContext.UserId.Value,
                foodStorageId = request.FoodStorageId
            });

            return result.AsList();
        }
    }
}
