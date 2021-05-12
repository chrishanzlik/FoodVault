using FoodVault.Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

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

        /// <inheritdoc />
        public Guid UserId
        {
            get
            {
                string subValue = _httpContextAccessor
                    .HttpContext?
                    .User?
                    .Claims?
                    .SingleOrDefault(x => x.Type.Equals("sub", StringComparison.OrdinalIgnoreCase))?
                    .Value;

                if (Guid.TryParse(subValue, out Guid id))
                {
                    return id;
                }

                return Guid.Empty;

                //throw new ApplicationException("No user context found.");
            }
        }

        /// <inheritdoc />
        public bool IsAvailable => _httpContextAccessor?.HttpContext != null;
    }
}
