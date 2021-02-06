using System;

namespace FoodVault.Framework.Application
{
    /// <summary>
    /// Interface that defines a method to access execution context (e.g. ASP.NET) data.
    /// </summary>
    public interface IExecutionContextAccessor
    {
        /// <summary>
        /// Gets the identifier of the current user.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Gets a value indicating whether the execution context is available.
        /// </summary>
        bool IsAvailable { get; }
    }
}
