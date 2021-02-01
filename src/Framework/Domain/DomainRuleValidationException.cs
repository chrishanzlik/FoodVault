namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Exception which can occur at domain rule validation.
    /// </summary>
    public class DomainRuleValidationException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRuleValidationException" /> class.
        /// </summary>
        /// <param name="domainRule">The failed rule.</param>
        public DomainRuleValidationException(IDomainRule domainRule) : base(domainRule.Message)
        {

        }
    }
}
