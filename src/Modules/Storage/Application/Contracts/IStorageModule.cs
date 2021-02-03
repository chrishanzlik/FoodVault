using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Queries;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Contracts
{
    public interface IStorageModule
    {
        Task<ICommandResult> ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
