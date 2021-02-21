using System;

namespace FoodVault.Modules.UserAccess.Application.Contracts
{
    public interface IUserAccessModuleUrlBuilder
    {
        string BuildConfirmationLink(Guid registrationId);
    }
}
