using Quartz;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.FileUploads
{
    /// <summary>
    /// Quartz job that triggers the temp file cleanup.
    /// </summary>
    [DisallowConcurrentExecution]
    internal class CleanupTempFilesJob : IJob
    {
        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandExecutor.ExecuteAsync(new RemoveExpiredFilesCommand());
        }
    }
}
