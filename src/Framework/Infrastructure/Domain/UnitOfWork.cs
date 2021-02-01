using FoodVault.Framework.Domain;
using FoodVault.Framework.Infrastructure.Work;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure.Domain
{
    /// <summary>
    /// Unit of work.
    /// </summary>
    /// <typeparam name="TContext">Type of the db context.</typeparam>
    public sealed class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="domainEventDispatcher">Domain event dispatcher.</param>
        public UnitOfWork(
            TContext context,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
        }

        /// <summary>
        /// Publishes all attached domain events, then write all changes to the given <see cref="TContext"/>.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Number of updated/changed records.</returns>
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
