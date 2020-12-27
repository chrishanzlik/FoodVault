using FoodVault.Application;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FoodVault.Infrastructure.Database
{
    /// <summary>
    /// Factory for creating SQL <see cref="IDbConnection"/>s.
    /// </summary>
    internal class SqlConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly string _connectionString;

        private IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConnectionFactory" /> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc />
        public IDbConnection GetOpen()
        {
            if (_connection?.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_connection?.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}
