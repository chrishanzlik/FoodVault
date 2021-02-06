using FoodVault.Framework.Application.Commands;
using System.Threading.Tasks;

namespace FoodVault.Framework.Application
{
    /// <summary>
    /// Schedules command(s) for later execution
    /// </summary>
    public interface ICommandScheduler
    {
        /// <summary>
        /// Schedules a <see cref="ICommand"/> for later exection.
        /// </summary>
        /// <param name="command">Command which should be scheduled.</param>
        /// <returns>Awaitable task.</returns>
        Task ScheduleAsync(ICommand command);
    }
}
