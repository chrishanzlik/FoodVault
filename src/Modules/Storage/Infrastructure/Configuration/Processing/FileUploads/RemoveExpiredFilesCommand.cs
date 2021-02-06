using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.FileUploads
{
    /// <summary>
    /// Command that removes expired files from the filestorage.
    /// </summary>
    internal class RemoveExpiredFilesCommand : Command, IRecurringCommand
    {
    }
}
