using Dapper;
using System;
using System.Data;

namespace FoodVault.Infrastructure.Database
{
    /// <summary>
    /// Class that provides specified UTC dates on loaded entities with dapper.
    /// </summary>
    public class DateTimeUtcDapperHandler : SqlMapper.TypeHandler<DateTime>
    {
        /// <inheritdoc />
        public override DateTime Parse(object value)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        }

        /// <inheritdoc />
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }
    }
}
