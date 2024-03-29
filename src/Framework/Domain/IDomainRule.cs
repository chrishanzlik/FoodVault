﻿namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Interface for domain rules.
    /// </summary>
    public interface IDomainRule
    {
        /// <summary>
        /// Triggers the validation mechanism.
        /// </summary>
        /// <returns>Boolean value if the rule passed without errors or not.</returns>
        bool Pass();

        /// <summary>
        /// Gets the validation message when the validation was unsuccessful.
        /// </summary>
        string Message { get; }
    }
}
