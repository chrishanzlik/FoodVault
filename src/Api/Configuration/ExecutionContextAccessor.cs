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

        //TODO: 
        public Guid UserId => Guid.Empty;

        //TODO: 
        public bool IsAvailable => true;
    }
}
