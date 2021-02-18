using FoodVault.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Modules.UserAccess.Domain.Users
{
    /// <summary>
    /// Identifier for a <see cref="User"/> object.
    /// </summary>
    public sealed class UserId : EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserId" /> class.
        /// </summary>
        /// <param name="value">Identifiers value.</param>
        public UserId(Guid value) : base(value)
        {
        }
    }
}
