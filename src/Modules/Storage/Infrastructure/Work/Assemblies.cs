using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using System.Reflection;

namespace FoodVault.Modules.Storage.Infrastructure.Work
{
    /// <summary>
    /// App assembly references.
    /// </summary>
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateStorageCommand).Assembly;
    }
}
