using FoodVault.Framework.Application.Queries;
using System;
using System.Collections.Generic;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageShares
{
    /// <summary>
    /// Query that fetches all shares of a specific food storage.
    /// </summary>
    public class GetStorageSharesQuery : IQuery<IEnumerable<StorageShareDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageSharesQuery" /> class.
        /// </summary>
        /// <param name="foodStorageId">Identifier of the food storage.</param>
        public GetStorageSharesQuery(Guid foodStorageId)
        {
            FoodStorageId = foodStorageId;
        }

        /// <summary>
        /// Gets the food storage identifier.
        /// </summary>
        public Guid FoodStorageId { get; }
    }
}
