using System;

namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Ignores members for equality comparison.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
