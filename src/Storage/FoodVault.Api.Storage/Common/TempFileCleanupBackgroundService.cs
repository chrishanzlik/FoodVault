using FoodVault.Application.FileUploads;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.Common
{
    public class TempFileCleanupBackgroundService : BackgroundService
    {
        private readonly ILogger<TempFileCleanupBackgroundService> _logger;
        private readonly IFileStorage _fileStorage;

        public TempFileCleanupBackgroundService(
            ILogger<TempFileCleanupBackgroundService> logger,
            IFileStorage fileStorage)
        {
            _logger = logger;
            _fileStorage = fileStorage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                try
                {
                    _logger.LogInformation("Start executing...");

                    await _fileStorage.DeleteExpiredFilesAsync();

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
