using Autofac;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Queries;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Infrastructure.Configuration;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing;
using MediatR;
using Microsoft.Extensions.Logging;
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
        #region Interface implementation

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
        public async Task<Guid> StoreFileTemporaryAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            TimeSpan expirationTime)
        {
            using var scope = StorageCompositionRoot.BeginLifetimeScope();
            var fileStorage = scope.Resolve<IFileStorage>();

            return await fileStorage.StoreFileTemporaryAsync(
                fileStream,
                fileName,
                contentType,
                expirationTime);
        }

        #endregion

        #region Setup

        /// <summary>
        /// Initializes the module. Internal container will be configured and composition root is ready.
        /// Quartz scheduler will be set up and started.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// <param name="executionContextAccessor">Accesor for application execution context.</param>
        /// <param name="fileUploadSettings">File upload settings.</param>
        /// <param name="urlBuilder">Url builder for the storage module.</param>
        /// <param name="logger">Application logger.</param>
        /// <param name="eventsBus">Applications event bus.</param>
        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IFileUploadSettings fileUploadSettings,
            IStorageModuleUrlBuilder urlBuilder,
            ILogger logger,
            IEventBus eventsBus)
        {
            StorageStartup.Initialize(
                connectionString,
                executionContextAccessor,
                fileUploadSettings,
                urlBuilder,
                logger,
                eventsBus);
        }

        /// <summary>
        /// Stops the quartz scheduler.
        /// </summary>
        public static void Shutdown()
        {
            StorageStartup.Stop();
        }

        #endregion
    }
}
