using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Queries;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.Contracts
{
    /// <summary>
    /// User access module.
    /// </summary>
    public interface IUserAccessModule
    {
        /// <summary>
        /// Executes a command to the user access module.
        /// </summary>
        /// <param name="command">Command to execution. Must be defined inside the user access module.</param>
        /// <returns>Result of the command execution.</returns>
        Task<ICommandResult> ExecuteCommandAsync(ICommand command);

        /// <summary>
        /// Requests a query from the user access module.
        /// </summary>
        /// <typeparam name="TResult">Type of the expected result.</typeparam>
        /// <param name="query">Query to execute.</param>
        /// <returns>Queries result.</returns>
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
