using MediatR;

namespace FoodVault.Application.Mediator
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
