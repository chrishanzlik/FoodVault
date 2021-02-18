using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    /// <summary>
    /// Determines in which state the <see cref="UserRegistration"/> is.
    /// </summary>
    public class RegistrationState : ValueObject
    {
        private RegistrationState(string state)
        {
            Value = state;
        }

        /// <summary>
        /// User did not confirm his registration yet.
        /// </summary>
        public static RegistrationState WaitingForConfirmation => new RegistrationState(nameof(WaitingForConfirmation));

        /// <summary>
        /// User has confirmed his registration.
        /// </summary>
        public static RegistrationState Confirmed => new RegistrationState(nameof(Confirmed));

        /// <summary>
        /// Registration is expired.
        /// </summary>
        public static RegistrationState Expired => new RegistrationState(nameof(Expired));

        /// <summary>
        /// Gets the value of the <see cref="RegistrationState"/>.
        /// </summary>
        public string Value { get; }
    }
}
