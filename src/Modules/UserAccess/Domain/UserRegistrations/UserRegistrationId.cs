using FoodVault.Framework.Domain;
using System;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    /// <summary>
    /// Identifier for a <see cref="UserRegistration"/> object.
    /// </summary>
    public sealed class UserRegistrationId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegistrationId" /> class.
        /// </summary>
        /// <param name="value">Identifiers value.</param>
        public UserRegistrationId(Guid value) : base(value)
        {
        }
    }
}
