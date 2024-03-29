﻿using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure
{
    /// <summary>
    /// Unit of work interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Write all changes made within the UoW context.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
