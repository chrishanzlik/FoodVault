using FoodVault.Application.Storage.FoodStorages.CreateStorage;
using System.Reflection;

namespace FoodVault.Infrastructure.Storage.Work
{
    /// <summary>
    /// App assembly references.
    /// </summary>
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateStorageCommand).Assembly;
    }
}
