﻿using FoodVault.Framework.Application;
using FoodVault.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using FoodVault.Modules.UserAccess.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVault.Api.Configuration.Authorization
{
    internal class HasPermissionAuthorizationHandler : AttributeAuthorizationHandler<HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IUserAccessModule _userAccessModule;

        public HasPermissionAuthorizationHandler(
            IExecutionContextAccessor executionContextAccessor,
            IUserAccessModule userAccessModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _userAccessModule = userAccessModule;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionAuthorizationRequirement requirement,
            HasPermissionAttribute attribute)
        {
            var permissions = await _userAccessModule.ExecuteQueryAsync(new GetUserPermissionsQuery(_executionContextAccessor.UserId));

            if (!await AuthorizeAsync(attribute.Name, permissions))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }

        private static Task<bool> AuthorizeAsync(string permission, IReadOnlyCollection<UserPermissionDto> permissions)
        {
            return Task.FromResult(permissions.Any(x => x.Permission == permission));
        }
    }
}
