using FoodVault.Application.Queries;
using System.Collections.Generic;

namespace FoodVault.Application.Storage.FoodStorages.GetStorageOverview
{
    /// <summary>
    /// Query that requests all available food storages.
    /// </summary>
    public class GetStorageOverviewQuery : IQuery<IEnumerable<FoodStorageDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageOverviewQuery" /> class.
        /// </summary>
        public GetStorageOverviewQuery()
        {
        }
    }
}
