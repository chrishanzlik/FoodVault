namespace FoodVault.Modules.Storage.Domain.Users
{
    /// <summary>
    /// Provides informations about the executing user.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the users identifier.
        /// </summary>
        UserId UserId { get; }
    }
}
