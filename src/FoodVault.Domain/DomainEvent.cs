using System;
using System.Collections.Generic;
using System.Text;

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
