namespace FoodVault.Domain
{
    public class DomainRuleValidationException : DomainException
    {
        public DomainRuleValidationException(IDomainRule domainRule)
        {

        }
    }
}
