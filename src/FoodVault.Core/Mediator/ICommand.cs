using MediatR;

namespace FoodVault.Core.Mediator
{
    public interface ICommand : IRequest<ICommandResult>
    {
    }
}
