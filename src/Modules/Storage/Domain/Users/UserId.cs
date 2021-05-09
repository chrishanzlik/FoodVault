using FoodVault.Framework.Domain;
using System;

namespace FoodVault.Modules.Storage.Domain.Users
{
    /// <summary>
    /// Identifier for a app user.
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
