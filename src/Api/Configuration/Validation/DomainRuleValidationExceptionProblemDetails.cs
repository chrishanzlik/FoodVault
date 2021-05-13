using FoodVault.Framework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodVault.Api.Configuration.Validation
{
    /// <summary>
    /// Describes a specific <see cref="DomainRuleValidationException"/>
    /// </summary>
    public class DomainRuleValidationExceptionProblemDetails : ProblemDetails
    {
        public DomainRuleValidationExceptionProblemDetails(DomainRuleValidationException exception)
        {
            Title = "Business rule broken";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Message;
            Type = "https://foodvault/business-rule-validation-error";
        }
    }
}
