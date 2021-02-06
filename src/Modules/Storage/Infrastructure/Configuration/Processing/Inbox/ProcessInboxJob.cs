using Quartz;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Inbox processing quartz job.
    /// </summary>
    [DisallowConcurrentExecution]
    internal class ProcessInboxJob : IJob
    {
        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandExecutor.ExecuteAsync(new ProcessInboxCommand());
        }
    }
}
