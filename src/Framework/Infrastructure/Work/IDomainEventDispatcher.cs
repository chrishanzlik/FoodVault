using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure.Work
{
    /// <summary>
    /// Interface for an object that dispatches all applicable domain events.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches all current attached domain events.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        Task DispatchEventsAsync();
    }
}
