using FoodVault.Framework.Application;
using FoodVault.Modules.Storage.Domain.Users;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages
{
    /// <summary>
    /// Provides informations about the executing user.
    /// </summary>
    public class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext" /> class.
        /// </summary>
        /// <param name="executionContextAccessor">Execution context accessor.</param>
        public UserContext(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }

        /// <inheritdoc />
        public UserId UserId
        {
            get
            {
                if (!_executionContextAccessor.IsAvailable)
                {
                    throw new InvalidOperationException("ExecutionContext is not available via the registered IExecutionContextAccessor.");
                }

                return new UserId(_executionContextAccessor.UserId);
            }
        }
    }
}
