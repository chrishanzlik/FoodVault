using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Work
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
