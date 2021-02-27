using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using System;
using System.Collections.Generic;
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

        public async Task<ICommandResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return CommandResult.AuthenticationFailed<UserDto>("Whoops, that's not implemented yet...");
        }
    }
}
