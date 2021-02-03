using System;

namespace FoodVault.Framework.Application
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        bool IsAvailable { get; }
    }
}
