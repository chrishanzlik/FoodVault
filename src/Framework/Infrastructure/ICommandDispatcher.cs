using System;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure
{
    /// <summary>
    /// Defines an object that dispatches commands.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a stored command by its identifier.
        /// </summary>
        /// <param name="id">Id of the command to dispatch.</param>
        /// <returns>Awaitable task.</returns>
        Task DispatchCommandAsync(Guid id);
    }
}
