using FoodVault.Infrastructure;
using FoodVault.Infrastructure.Outbox;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.Common
{
    public class OutboxProcessingBackgroundService : BackgroundService
    {
        private readonly ICommandExecutor _isolatedCommandExecutor;
        private readonly ILogger<OutboxProcessingBackgroundService> _logger;

        public OutboxProcessingBackgroundService(
            ICommandExecutor isolatedCommandExecutor,
            ILogger<OutboxProcessingBackgroundService> logger)
        {
            _isolatedCommandExecutor = isolatedCommandExecutor;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));

                try
                {
                    _logger.LogInformation("Start executing...");

                    await _isolatedCommandExecutor.Execute(new ProcessOutboxCommand());
                    
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
