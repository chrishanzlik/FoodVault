using FoodVault.Framework.Application.Queries;
using System.Collections.Generic;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesOverview
{
    /// <summary>
    /// Query that requests all available food storages.
    /// </summary>
    public class GetStorageOverviewQuery : IQuery<IEnumerable<FoodStorageDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStorageOverviewQuery" /> class.
        /// </summary>
        /// <param name="nameFilter">Optional filtering for storage name.</param>
        public GetStorageOverviewQuery(
            string nameFilter = null)
        {
            NameFilter = nameFilter;
        }

        /// <summary>
        /// Gets the name filter value.
        /// </summary>
        public string NameFilter { get; }
    }
}
