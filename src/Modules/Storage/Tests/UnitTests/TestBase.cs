using FoodVault.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodVault.Modules.Storage.Tests.UnitTests
{
    public abstract class TestBase : IDisposable
    {
        public static TEvent AssertPublishedDomainEvent<TEvent>(Entity entity) where TEvent : DomainEvent
        {
            var domainEvent = entity.DomainEvents.OfType<TEvent>().FirstOrDefault();
            if (domainEvent == null)
            {
                throw new Exception($"The domain event of type {typeof(TEvent).Name} was not published.");
            }

            return domainEvent;
        }

        public static List<TEvent> AssertPublishedDomainEvents<TEvent>(Entity entity) where TEvent : DomainEvent
        {
            var domainEvents = entity.DomainEvents.OfType<TEvent>().ToList();
            if (domainEvents.Count == 0)
            {
                throw new Exception($"The domain event of type {typeof(TEvent).Name} was not published.");
            }

            return domainEvents;
        }

        public static void AssertNotPublishedDomainEvent<TEvent>(Entity entity) where TEvent : DomainEvent
        {
            var domainEvent = entity.DomainEvents.OfType<TEvent>().SingleOrDefault();
            Assert.Null(domainEvent);
        }

        public static void AssertBrokenDomainRule<TRule>(Action testCode)
        {
            var exception = Assert.Throws<DomainRuleValidationException>(testCode);
            if (exception != null)
            {
                Assert.IsType<TRule>(exception);
            }
        }

        public void Dispose()
        {
        }
    }
}
