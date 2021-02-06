using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Command that triggers inbox processing.
    /// </summary>
    public class ProcessInboxCommand : Command, IRecurringCommand
    {
    }
}
