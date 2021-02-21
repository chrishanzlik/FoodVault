using FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser;
using System.Reflection;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration
{
    /// <summary>
    /// App assembly references.
    /// </summary>
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(RegisterUserCommand).Assembly;
    }
}
