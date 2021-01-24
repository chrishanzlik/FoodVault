using MediatR;

namespace FoodVault.Application.Queries
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
