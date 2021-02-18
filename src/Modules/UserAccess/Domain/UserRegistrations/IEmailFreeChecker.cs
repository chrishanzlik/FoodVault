namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    public interface IEmailFreeChecker
    {
        bool EmailCanBeUsed(string email);
    }
}
