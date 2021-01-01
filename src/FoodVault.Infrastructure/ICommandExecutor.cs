using FoodVault.Application.Mediator;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure
{
    public interface ICommandExecutor
    {
        Task<ICommandResult> Execute(ICommand command);
    }
}
