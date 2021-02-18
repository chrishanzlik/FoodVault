using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    /// <summary>
    /// Rule that checks that a registration cannot be confirmed after it is expired.
    /// </summary>
    internal class RegistrationCannotBeConfirmedWhenExpiredRule : IDomainRule
    {
        private readonly RegistrationState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationCannotBeConfirmedWhenExpiredRule" /> class.
        /// </summary>
        /// <param name="state">Current state of the registration.</param>
        public RegistrationCannotBeConfirmedWhenExpiredRule(RegistrationState state)
        {
            _state = state;
        }

        /// <inheritdoc />
        public string Message => "Registration cannot be confirmed after expiration.";

        /// <inheritdoc />
        public bool Pass() => _state != RegistrationState.Expired;
    }
}
