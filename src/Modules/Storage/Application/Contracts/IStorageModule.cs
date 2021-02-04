using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Queries;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Contracts
{
    public interface IStorageModule
    {
        Task<ICommandResult> ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);

        Task<Guid> StoreFileTemporaryAsync(Stream fileStream, string fileName, string contentType, TimeSpan expirationTime);
    }
}
