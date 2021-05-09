using FoodVault.Framework.Application.Queries;
using System;
using System.Collections.Generic;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageContent
{
    /// <summary>
    /// Retrieves the stored products from a product storage.
    /// </summary>
    public class GetStorageContentQuery : IQuery<IEnumerable<StoredProductDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageContentQuery" /> class.
        /// </summary>
        /// <param name="foodStorageId">FoodStorage identifier.</param>
        public GetStorageContentQuery(Guid foodStorageId)
        {
            FoodStorageId = foodStorageId;
        }

        /// <summary>
        /// Gets the identifier of the food storage.
        /// </summary>
        public Guid FoodStorageId { get; }
    }
}
