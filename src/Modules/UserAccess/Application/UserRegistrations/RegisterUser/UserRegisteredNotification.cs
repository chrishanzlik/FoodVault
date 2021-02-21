using FoodVault.Framework.Application.Events;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations.Events;
using Newtonsoft.Json;
using System;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser
{
    public class UserRegisteredNotification : DomainEventNotification<UserRegisteredEvent>
    {
        [JsonConstructor]
        public UserRegisteredNotification(UserRegisteredEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}
