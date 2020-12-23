using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Domain
{
    public interface IDomainRule
    {
        bool Validate();
    }
}
