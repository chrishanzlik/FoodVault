using FoodVault.Framework.Application.Queries;
using System;
using System.Collections.Generic;

namespace FoodVault.Modules.UserAccess.Application.Authorization.GetUserPermissions
{
    public class GetUserPermissionsQuery : IQuery<IReadOnlyCollection<UserPermissionDto>>
    {
        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
