using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodVault.Application
{
    public interface ICommandScheduler
    {
        Task ScheduleAsync<T>(ICommand command);
    }
}
