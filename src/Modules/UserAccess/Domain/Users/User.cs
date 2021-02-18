using FoodVault.Framework.Domain;
using FoodVault.Modules.UserAccess.Domain.Users.Events;
using System;
using System.Collections.Generic;

namespace FoodVault.Modules.UserAccess.Domain.Users
{
    /// <summary>
    /// User aggregate root.
    /// </summary>
    public class User : Entity, IAggregateRoot
    {
        private string _firstName;

        private string _lastName;

        private bool _isActive;

        private EmailAddress _email;

        private List<UserRole> _roles;

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private User()
        {
        }

        private User(string email, string firstName, string lastName)
        {
            Id = new UserId(Guid.NewGuid());

            _firstName = firstName;
            _lastName = lastName;
            _isActive = true;
            _email = new EmailAddress(email);

            _roles = new List<UserRole>();
            _roles.Add(UserRole.Member);

            this.AddDomainEvent(new UserCreatedEvent(Id));
        }

        public UserId Id { get; private set; }

        public static User CreateNew(string email, string firstName, string lastName)
        {
            return new User(email, firstName, lastName);
        }
    }
}
