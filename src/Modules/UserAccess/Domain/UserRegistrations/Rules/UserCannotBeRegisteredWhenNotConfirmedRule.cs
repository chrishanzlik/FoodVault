using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    /// <summary>
    /// Rule that checks that a given user cannot be created when the registration was not confirmed.
    /// </summary>
    internal class UserCannotBeRegisteredWhenNotConfirmedRule : IDomainRule
    {
        private readonly RegistrationState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCannotBeRegisteredWhenNotConfirmedRule" /> class.
        /// </summary>
        /// <param name="state">Current state of the registration.</param>
        public UserCannotBeRegisteredWhenNotConfirmedRule(RegistrationState state)
        {
            _state = state;
        }

        /// <inheritdoc />
        public string Message => "User registration is not confirmed yet.";

        /// <inheritdoc />
        public bool Pass() => _state == RegistrationState.Confirmed;
    }
}
