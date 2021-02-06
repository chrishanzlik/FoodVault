using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Queries;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Contracts
{
    /// <summary>
    /// Storage module.
    /// </summary>
    public interface IStorageModule
    {
        /// <summary>
        /// Executes a command to the storage module.
        /// </summary>
        /// <param name="command">Command to execution. Must be defined inside the storage module.</param>
        /// <returns>Result of the command execution.</returns>
        Task<ICommandResult> ExecuteCommandAsync(ICommand command);

        /// <summary>
        /// Requests a query from the storage module.
        /// </summary>
        /// <typeparam name="TResult">Type of the expected result.</typeparam>
        /// <param name="query">Query to execute.</param>
        /// <returns>Queries result.</returns>
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);

        /// <summary>
        /// Stores a temporary file to the storage module.
        /// </summary>
        /// <param name="fileStream">Files stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Files content type.</param>
        /// <param name="expirationTime">Date when the file expires.</param>
        /// <returns>Identifier of the newly created temporary file.</returns>
        Task<Guid> StoreFileTemporaryAsync(Stream fileStream, string fileName, string contentType, TimeSpan expirationTime);
    }
}
