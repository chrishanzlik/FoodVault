using MediatR;

namespace FoodVault.Application.Commands
{
    /// <summary>
    /// Interface that defines a application command.
    /// </summary>
    public interface ICommand : IRequest<ICommandResult>
    {
    }
}
