using MediatR;

namespace FoodVault.Framework.Application.Queries
{
    /// <summary>
    /// Interface which represents a user query.
    /// </summary>
    /// <typeparam name="TResult">Type of the query result.</typeparam>
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
