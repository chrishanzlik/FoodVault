using MediatR;

namespace FoodVault.Framework.Application.Queries
{
    /// <summary>
    /// Query handler interface for <see cref="IQuery{TResult}"/>s.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query.</typeparam>
    /// <typeparam name="TResult">Type of the query result.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
