using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodVault.Infrastructure.Storage.Database
{
    /// <summary>
    /// Database seed for test purposes.
    /// </summary>
    public static class Seed
    {
        /// <summary>
        /// Applies the seed to the given db context.
        /// </summary>
        /// <param name="context">Db context.</param>
        public static void Apply(StorageContext context)
        {
#if DEBUG
            SeedStorages(context);
            SeedProducts(context);
#endif
        }

        private static void SeedStorages(StorageContext context)
        {
            if (!context.FoodStorages.Any())
            {
                var storageSeed = new[]
                {
                    new FoodStorage("Test-Storage", "Initial test storage.")
                };
                context.FoodStorages.AddRange(storageSeed);
                context.SaveChanges();
            }
        }

        private static void SeedProducts(StorageContext context)
        {
            if (!context.Products.Any())
            {
                var productSeed = new[]
                {
                    new Product("Dosentomaten 500g", "Ja!"),
                    new Product("Eier 12St.", "Freilandhof Foobar"),
                    new Product("Milch 1l"),
                    new Product("Salamiaufschnitt 200g", "Ja!"),
                    new Product("Pizza-Mehl 500g", "Caputo")
                };
                context.Products.AddRange(productSeed);
                context.SaveChanges();
            }
        }
    }
}
