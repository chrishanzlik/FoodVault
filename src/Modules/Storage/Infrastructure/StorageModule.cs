using Autofac;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Queries;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Infrastructure.Configuration;
using FoodVault.Modules.Storage.Infrastructure.Work;
using MediatR;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure
{
    public class StorageModule : IStorageModule
    {
        public async Task<ICommandResult> ExecuteCommandAsync(ICommand command)
        {
            return await CommandExecutor.ExecuteAsync(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = StorageCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
