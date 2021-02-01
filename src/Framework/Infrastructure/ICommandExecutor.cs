using FoodVault.Framework.Application.Commands;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure
{
    /// <summary>
    /// Provides an object that execute <see cref="ICommand"/>s.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes a command within a defined scope.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Execution result.</returns>
        Task<ICommandResult> Execute(ICommand command);
    }
}
