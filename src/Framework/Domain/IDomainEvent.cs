using MediatR;
using System;

namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Interface for most basic domain events.
    /// </summary>
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// Gets the identifier of the occuring domain event.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the date and time when the event occured at.
        /// </summary>
        DateTime RaisedAt { get; }
    }
}
