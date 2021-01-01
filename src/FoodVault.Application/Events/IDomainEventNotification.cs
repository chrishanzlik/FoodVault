using MediatR;
using System;

namespace FoodVault.Application.Events
{
    /// <summary>
    /// Notification about an occured domain event.
    /// </summary>
    /// <typeparam name="TEvent">Type of the domain event.</typeparam>
    public interface IDomainEventNotification<out TEvent> : IDomainEventNotification
    {
        /// <summary>
        /// Gets the associated domain event.
        /// </summary>
        TEvent DomainEvent { get; }
    }

    /// <summary>
    /// Notification about an occured domain event.
    /// </summary>
    public interface IDomainEventNotification : INotification
    {
        /// <summary>
        /// Gets the id of the notification.
        /// </summary>
        Guid Id { get; }
    }
}
