using MediatR;

namespace FoodVault.Core.Mediator
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
