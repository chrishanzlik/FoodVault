using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.DomainServices
{
    public class EmailFreeChecker : IEmailFreeChecker
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public EmailFreeChecker(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public bool IsFreeEmail(string email)
        {
            string existsSql =
                $"SELECT " +
                $"COUNT(DISTINCT 1) " +
                $"FROM [users].[UserRegistrations] " +
                $"WHERE UPPER([EmailAddress]) = @email AND NOT ([State] = '{RegistrationState.Expired.Value}')";

            var connection = _dbConnectionFactory.GetOpen();
            return !connection.ExecuteScalar<bool>(existsSql, new { email = email.ToUpper() });
        }
    }
}
