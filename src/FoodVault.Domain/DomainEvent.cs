using System;

namespace FoodVault.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            RaisedAt = DateTime.UtcNow;
        }

        public DateTime RaisedAt { get; }
    }
}
