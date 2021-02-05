using FoodVault.Framework.Application.Commands;
using System.Threading.Tasks;

namespace FoodVault.Framework.Application
{
    public interface ICommandScheduler
    {
        Task ScheduleAsync<T>(ICommand command);
    }
}
