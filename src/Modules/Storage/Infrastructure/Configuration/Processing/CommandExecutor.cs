using Autofac;
using FoodVault.Framework.Application.Commands;
using MediatR;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing
{
    /// <summary>
    /// Executes command within a own scope.
    /// </summary>
    public static class CommandExecutor
    {
        /// <summary>
        /// Executes the given <see cref="ICommand"/> within a own scope.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Commands execution result.</returns>
        internal static async Task<ICommandResult> ExecuteAsync(ICommand command)
        {
            using var scope = StorageCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }
    }
}