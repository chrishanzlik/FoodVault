using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Api.Common
{
    public class TempFileCleanupBackgroundService : BackgroundService
    {
        private readonly ILogger<TempFileCleanupBackgroundService> _logger;
        private readonly ICommandExecutor _isolatedCommandExecutor;

        public TempFileCleanupBackgroundService(
            ILogger<TempFileCleanupBackgroundService> logger,
            ICommandExecutor commandExecutor)
        {
            _logger = logger;
            _isolatedCommandExecutor = commandExecutor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                try
                {
                    _logger.LogInformation("Start executing...");

                    await _isolatedCommandExecutor.Execute(new RemoveExpiredFilesCommand());

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
