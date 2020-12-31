using System;

namespace FoodVault.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            RaisedAt = DateTime.Now;
        }

        public DateTime RaisedAt { get; }
    }
}
