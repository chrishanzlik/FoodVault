using FoodVault.Framework.Application;
using FoodVault.Modules.Storage.Domain.Users;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages
{
    public class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

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
