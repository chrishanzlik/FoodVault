using FoodVault.Framework.Application;
using Microsoft.AspNetCore.Http;
using System;

namespace FoodVault.Api.Configuration.ExecutionContext
{
    internal class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => throw new NotImplementedException();

        public bool IsAvailable => throw new NotImplementedException();
    }
}
