using System;

namespace FoodVault.Api.Configuration.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NoPermissionRequiredAttribute : Attribute
    {
    }
}
