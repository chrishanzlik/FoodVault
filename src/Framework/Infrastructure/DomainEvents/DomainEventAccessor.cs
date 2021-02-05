using FoodVault.Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    public class DomainEventAccessor : IDomainEventAccessor
    {
        private readonly DbContext _dbContext;

        public DomainEventAccessor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ClearAllDomainEvents()
        {
            var entities = GetDomainEntities(_dbContext);

            foreach(var entity in entities)
            {
                entity.ClearDomainEvents();
            }
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var entities = GetDomainEntities(_dbContext);

            return entities.SelectMany(x => x.DomainEvents).ToList();
        }

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
