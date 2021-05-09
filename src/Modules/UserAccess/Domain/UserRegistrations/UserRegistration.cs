using FoodVault.Framework.Domain;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations.Events;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations.Rules;
using FoodVault.Modules.UserAccess.Domain.Users;
using System;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    /// <summary>
    /// Represents a user registration. This process leads to an registered user.
    /// </summary>
    public class UserRegistration : Entity, IAggregateRoot
    {
        private EmailAddress _email;
        private RegistrationState _state;
        private DateTime? _confirmedAt;
        private PasswordHash _passwordHash;
        private string _firstName;
        private string _lastName;

        /// <summary>
        /// Required by Entity Framework Core.
        /// </summary>
        private UserRegistration()
        {
        }

        private UserRegistration(EmailAddress email, PasswordHash hash, string firstName, string lastName, IEmailFreeChecker emailFreeChecker)
        {
            this.CheckDomainRule(new UserEmailNotInUseRule(emailFreeChecker, email));

            Id = new UserRegistrationId(Guid.NewGuid());
            _email = email;
            _passwordHash = hash;
            _firstName = firstName;
            _lastName = lastName;
            _state = RegistrationState.WaitingForConfirmation;

            this.AddDomainEvent(new UserRegisteredEvent(Id, _email));
        }

        /// <summary>
        /// Gets the registration id;
        /// </summary>
        public UserRegistrationId Id { get; }

        /// <summary>
        /// Creates a new user registration.
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="hash">Users password hash</param>
        /// <param name="firstName">Users first name</param>
        /// <param name="lastName">Users last name</param>
        /// <param name="emailFreeChecker">Domain service that checks the uniquess of email addresses.</param>
        /// <returns></returns>
        public static UserRegistration RegisterUser(EmailAddress email, PasswordHash hash, string firstName, string lastName, IEmailFreeChecker emailFreeChecker)
        {
            return new UserRegistration(email, hash, firstName, lastName, emailFreeChecker);
        }

        /// <summary>
        /// Creates a user for the given registration.
        /// </summary>
        /// <returns>New created user.</returns>
        public User CreateUser()
        {
            this.CheckDomainRule(new UserCannotBeRegisteredWhenNotConfirmedRule(_state));

            return User.CreateForRegistration(Id, _email, _passwordHash, _firstName, _lastName);
        }

        /// <summary>
        /// Confirms the registration.
        /// </summary>
        public void Confirm()
        {
            this.CheckDomainRule(new RegistrationCannotBeConfirmedMultipleTimesRule(_state));
            this.CheckDomainRule(new RegistrationCannotBeConfirmedWhenExpiredRule(_state));

            _confirmedAt = DateTime.UtcNow;
            _state = RegistrationState.Confirmed;

            this.AddDomainEvent(new UserRegistrationConfirmedEvent(Id));
        }

        /// <summary>
        /// Expires the registration.
        /// </summary>
        public void Expire()
        {
            this.CheckDomainRule(new RegistrationCannotBeExpiredMultipleTimesRule(_state));

            _state = RegistrationState.Expired;

            this.AddDomainEvent(new UserRegistrationExpiredEvent(Id));
        }
    }
}
