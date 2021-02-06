using FoodVault.Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    /// <summary>
    /// Class for accessing domain events, which are attached to entities.
    /// </summary>
    public class DomainEventAccessor : IDomainEventAccessor
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventAccessor" /> class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public DomainEventAccessor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public void ClearAllDomainEvents()
        {
            var entities = GetDomainEntities(_dbContext);

            foreach(var entity in entities)
            {
                entity.ClearDomainEvents();
            }
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var entities = GetDomainEntities(_dbContext);

            return entities.SelectMany(x => x.DomainEvents).ToList();
        }

        /// <inheritdoc />
        private static IEnumerable<Entity> GetDomainEntities(DbContext context)
        {
            return context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true)
                .Select(x => x.Entity)
                .ToList();
        }
    }
}
