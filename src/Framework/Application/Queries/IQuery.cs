using MediatR;

namespace FoodVault.Framework.Application.Queries
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
