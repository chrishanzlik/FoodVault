using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.Authorization.GetUserPermissions
{
    internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, IReadOnlyCollection<UserPermissionDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IReadOnlyCollection<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //TODO: Impl Query
            const string sql =
                "TODO... ";

            var connection = _dbConnectionFactory.GetOpen();

            var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new { request.UserId });

            return permissions.AsList().AsReadOnly();
        }
    }
}

