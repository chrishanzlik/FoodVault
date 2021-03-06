using Dapper;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.UserAccess.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Application.Authentication.Authenticate
{
    /// <summary>
    /// Command handler for the <see cref="AuthenticateCommand"/>.
    /// </summary>
    internal class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IPasswordManager _passwordManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateCommandHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="passwordManager"></param>
        public AuthenticateCommandHandler(IDbConnectionFactory dbConnectionFactory, IPasswordManager passwordManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _passwordManager = passwordManager;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT " +
                "[User].[Id], " +
                "[User].[EmailAddress] AS [Email], " +
                "[User].[PasswordHash], " +
                "[User].[IsActive], " +
                "[User].[FirstName], " +
                "[User].[LastName] " +
                "FROM [users].[Users] AS [User] " +
                "WHERE [User].[EmailAddress] = @email";

            var connection = _dbConnectionFactory.GetOpen();
            var userDto = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { email = request.Email });

            if (userDto == null)
            {
                return CommandResult.AuthenticationFailed<UserDto>("Invalid email or password.");
            }

            if (!userDto.IsActive)
            {
                return CommandResult.AuthenticationFailed<UserDto>("User is not active.");
            }

            if (!_passwordManager.HashPassword(request.Password).Equals(userDto.PasswordHash))
            {
                return CommandResult.AuthenticationFailed<UserDto>("Invalid email or password.");
            }

            userDto.Claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.FirstName, userDto.FirstName),
                new Claim(CustomClaimTypes.LastName, userDto.LastName),
                new Claim(CustomClaimTypes.Email, userDto.Email)
            };

            return CommandResult.Authenticated(userDto);
        }
    }
}
