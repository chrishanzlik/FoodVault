using Dapper;
using System;
using System.Data;

namespace FoodVault.Infrastructure.Database
{
    public class DateTimeUtcDapperHandler : SqlMapper.TypeHandler<DateTime>
    {
        public override DateTime Parse(object value)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        }

        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }
    }
}
