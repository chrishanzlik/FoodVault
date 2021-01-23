using Dapper;
using System;
using System.Data;

namespace FoodVault.Infrastructure.Database
{
    public class NullableDateTimeUtcDapperHandler : SqlMapper.TypeHandler<DateTime?>
    {
        public override DateTime? Parse(object value)
        {
            if (value == null)
            {
                return null;
            }

            return DateTime.SpecifyKind(((DateTime?)value).Value, DateTimeKind.Utc);
        }

        public override void SetValue(IDbDataParameter parameter, DateTime? value)
        {
            parameter.Value = value;
        }
    }
}
