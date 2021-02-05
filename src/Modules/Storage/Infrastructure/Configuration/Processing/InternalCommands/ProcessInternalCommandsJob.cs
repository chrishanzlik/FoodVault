using Quartz;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Quartz job that triggers the internal command processing.
    /// </summary>
    [DisallowConcurrentExecution]
    internal class ProcessInternalCommandsJob : IJob
    {
        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandExecutor.ExecuteAsync(new ProcessInternalCommandsCommand());
        }
    }
}
