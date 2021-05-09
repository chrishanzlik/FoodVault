using FoodVault.Framework.Application;
using Microsoft.AspNetCore.Http;
using System;

namespace FoodVault.Api.Configuration.ExecutionContext
{
    /// <summary>
    /// Grants access to the current execution context.
    /// </summary>
    internal class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextAccessor" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor.</param>
        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //TODO: Implement ExecutionContextAccessor

        /// <inheritdoc />
        public Guid UserId => Guid.Empty;

        /// <inheritdoc />
        public bool IsAvailable => _httpContextAccessor?.HttpContext != null;
    }
}
