﻿using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Modules.Storage.Domain.Users;
using System;
using System.Linq;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess
{
    /// <summary>
    /// Database seed for test purposes.
    /// </summary>
    internal static class Seed
    {
        /// <summary>
        /// Applies the seed to the given db context.
        /// </summary>
        /// <param name="context">Db context.</param>
        public static void Apply(StorageContext context)
        {
#if DEBUG
            // Stop reseed every startup...
            if (context.FoodStorages.Any())
            {
                return;
            }

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
                    FoodStorage.CreateForUser(new UserId(Guid.Empty), "Test-Storage", "Initial test storage.", new FakeStorageNameUniquessChecker())
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
            var fakeUserContext = new FakeUserContext();
            var rnd = new Random();
            var storages = context.FoodStorages.ToList();
            var products = context.Products.ToList();

            foreach(var storage in storages)
            {
                if (storage.StoredProducts.Any()) continue;

                var prodCount = rnd.Next(products.Count / 2, products.Count);
                var productsToAdd = products
                    .OrderBy(x => rnd.Next())
                    .Take(prodCount)
                    .ToList();

                foreach(var prod in productsToAdd)
                {
                    storage.StoreProduct(prod.Id, rnd.Next(1, 13), fakeUserContext, null);
                }
            }

            context.SaveChanges();
        }

        private class FakeStorageNameUniquessChecker : IStorageNameUniquessChecker
        {
            public bool IsNameUniqueForUser(string storageName, Guid userId) => true;
        }

        private class FakeUserContext : IUserContext
        {
            public UserId UserId => new UserId(Guid.Empty);
        }
    }
}
