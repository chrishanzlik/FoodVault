using System.Data;

namespace FoodVault.Framework.Application.DataAccess
{
    /// <summary>
    /// Interface of a factory for creating <see cref="IDbConnection"/>s.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Retuns a opened <see cref="IDbConnection"/> when invoked.
        /// The connection may be reused depending on the implementation.
        /// </summary>
        /// <returns><see cref="IDbConnection"/> object.</returns>
        IDbConnection GetOpen();

        /// <summary>
        /// Retuns a new created and open <see cref="IDbConnection"/> when invoked.
        /// </summary>
        /// <returns><see cref="IDbConnection"/> object.</returns>
        IDbConnection CreateNew();
    }
}
