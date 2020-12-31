using MediatR;

namespace FoodVault.Application.Mediator
{
    public interface ICommand : IRequest<ICommandResult>
    {
    }
}
