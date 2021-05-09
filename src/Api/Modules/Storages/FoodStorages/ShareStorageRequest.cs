using System;

namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    /// <summary>
    /// Request data for sharing a storage to another user.
    /// </summary>
    public class ShareStorageRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the added user has write access.
        /// </summary>
        public bool WriteAccess { get; set; }
    }
}
