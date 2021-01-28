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
            FillStorages(context);
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
                    new Product("Pizza-Mehl 500g", "Caputo"),
                    new Product("Wasser", "Rhön Sprudel"),
                    new Product("Kaffeebohnen Crema", "Lavazza"),
                    new Product("Schwarzer Pfeffer", "Ja!")
                };
                context.Products.AddRange(productSeed);
                context.SaveChanges();
            }
        }

        private static void FillStorages(StorageContext context)
        {
            var rnd = new Random();
            var storages = context.FoodStorages.ToList();
            var products = context.Products.ToList();
            var checker = new FakeProductExistsChecker();

            foreach(var storage in storages)
            {
                var prodCount = rnd.Next(products.Count / 2, products.Count);
                var productsToAdd = products
                    .Select(x => new { value = x, order = rnd.Next() })
                    .OrderBy(x => x.order)
                    .Select(x => x.value)
                    .Take(prodCount)
                    .ToList();

                foreach(var prod in productsToAdd)
                {
                    storage.StoreProduct(prod.Id, rnd.Next(1, 13), null, checker);
                }
            }

            context.SaveChanges();
        }

        private class FakeProductExistsChecker : IProductExistsChecker
        {
            public bool ProductExists(ProductId id) => true;
        }
    }
}
