using MediatR;

namespace FoodVault.Application.Commands
{
    public interface ICommand : IRequest<ICommandResult>
    {
    }
}
