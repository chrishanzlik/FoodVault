using Autofac;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Queries;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Infrastructure.Configuration;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure
{
    /// <summary>
    /// Storage module.
    /// </summary>
    public class StorageModule : IStorageModule
    {
        /// <inheritdoc />
        public async Task<ICommandResult> ExecuteCommandAsync(ICommand command)
        {
            return await CommandExecutor.ExecuteAsync(command);
        }

        /// <inheritdoc />
        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using var scope = StorageCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            return await mediator.Send(query);
        }

        /// <inheritdoc />
        public async Task<Guid> StoreFileTemporaryAsync(Stream fileStream, string fileName, string contentType, TimeSpan expirationTime)
        {
            using var scope = StorageCompositionRoot.BeginLifetimeScope();
            var fileStorage = scope.Resolve<IFileStorage>();

            return await fileStorage.StoreFileTemporaryAsync(fileStream, fileName, contentType, expirationTime);
        }
    }
}
