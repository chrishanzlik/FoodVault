namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    public interface IEmailFreeChecker
    {
        bool IsFreeEmail(string email);
    }
}
