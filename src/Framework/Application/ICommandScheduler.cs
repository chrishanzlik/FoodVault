using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodVault.Framework.Application
{
    public interface ICommandScheduler
    {
        Task ScheduleAsync<T>(ICommand command);
    }
}
