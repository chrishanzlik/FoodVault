using FoodVault.Framework.Domain;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
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

        private PasswordHash _passwordHash;

        private EmailAddress _email;

        private List<UserRole> _roles;

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private User()
        {
        }

        private User(Guid id, EmailAddress email, PasswordHash hash, string firstName, string lastName)
        {
            Id = new UserId(id);
            _firstName = firstName;
            _lastName = lastName;
            _passwordHash = hash;
            _email = email;
            _isActive = true;

            _roles = new List<UserRole>
            {
                UserRole.Member
            };

            this.AddDomainEvent(new UserCreatedEvent(Id));
        }

        /// <summary>
        /// Gets the <see cref="User"/>s identifier.
        /// </summary>
        public UserId Id { get; private set; }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        internal static User CreateForRegistration(UserRegistrationId registrationId, EmailAddress email, PasswordHash hash, string firstName, string lastName)
        {
            return new User(registrationId, email, hash, firstName, lastName);
        }
    }
}
