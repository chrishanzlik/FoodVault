using Dapper;
using System;
using System.Data;

namespace FoodVault.Infrastructure.Database
{
    /// <summary>
    /// Class that provides specified UTC dates on loaded nullable entities with dapper.
    /// </summary>
    public class NullableDateTimeUtcDapperHandler : SqlMapper.TypeHandler<DateTime?>
    {
        /// <inheritdoc />
        public override DateTime? Parse(object value)
        {
            if (value == null)
            {
                return null;
            }

            return DateTime.SpecifyKind(((DateTime?)value).Value, DateTimeKind.Utc);
        }

        /// <inheritdoc />
        public override void SetValue(IDbDataParameter parameter, DateTime? value)
        {
            parameter.Value = value;
        }
    }
}
