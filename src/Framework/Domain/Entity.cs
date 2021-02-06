using System.Collections.Generic;

namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Base class for domain entities.
    /// </summary>
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        public Entity()
        {
            
        }

        /// <summary>
        /// Gets a lazy initialized list of attached <see cref="IDomainEvent"/>s.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// Queues a domain event to publish.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent != null)
            {
                if (_domainEvents == null)
                {
                    _domainEvents = new List<IDomainEvent>();
                }

                _domainEvents.Add(domainEvent);
            }
        }

        /// <summary>
        /// Clears all internally attached <see cref="IDomainEvent"/>s.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Check against the given <see cref="IDomainRule"/>. Throws on validation error.
        /// </summary>
        /// <param name="domainRule">Rule to check.</param>
        public void CheckDomainRule(IDomainRule domainRule)
        {
            if (!domainRule.Validate())
            {
                throw new DomainRuleValidationException(domainRule);
            }
        }
    }
}
