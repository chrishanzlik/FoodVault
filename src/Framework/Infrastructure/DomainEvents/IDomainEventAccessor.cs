using FoodVault.Framework.Domain;
using System.Collections.Generic;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    public interface IDomainEventAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
