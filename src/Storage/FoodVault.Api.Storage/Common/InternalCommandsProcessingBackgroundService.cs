using FoodVault.Infrastructure;
using FoodVault.Infrastructure.InternalCommands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.Common
{
    public class InternalCommandsProcessingBackgroundService : BackgroundService
    {
        private readonly ICommandExecutor _isolatedCommandExecutor;
        private readonly ILogger<InternalCommandsProcessingBackgroundService> _logger;

        public InternalCommandsProcessingBackgroundService(
            ICommandExecutor isolatedCommandExecutor,
            ILogger<InternalCommandsProcessingBackgroundService> logger)
        {
            _isolatedCommandExecutor = isolatedCommandExecutor;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(20));

                try
                {
                    _logger.LogInformation("Start executing...");

                    await _isolatedCommandExecutor.Execute(new ProcessInternalCommandsCommand());

                    _logger.LogInformation("Done.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
