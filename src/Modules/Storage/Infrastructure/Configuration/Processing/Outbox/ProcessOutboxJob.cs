using Quartz;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Quartz job that triggers the outbox processing.
    /// </summary>
    [DisallowConcurrentExecution]
    internal class ProcessOutboxJob : IJob
    {
        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandExecutor.ExecuteAsync(new ProcessOutboxCommand());
        }
    }
}
