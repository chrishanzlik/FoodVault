namespace FoodVault.Modules.UserAccess.Application.Authentication
{
    public interface IPasswordManager
    {
        public string HashPassword(string password);
    }
}
