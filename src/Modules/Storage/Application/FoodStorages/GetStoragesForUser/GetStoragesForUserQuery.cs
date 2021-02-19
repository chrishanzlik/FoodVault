using FoodVault.Framework.Application.Queries;
using System.Collections.Generic;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesForUser
{
    /// <summary>
    /// Query that requests all available food storages.
    /// </summary>
    public class GetStoragesForUserQuery : IQuery<IEnumerable<FoodStorageDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStoragesForUserQuery" /> class.
        /// </summary>
        /// <param name="nameFilter">Optional filtering for storage name.</param>
        public GetStoragesForUserQuery(
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
