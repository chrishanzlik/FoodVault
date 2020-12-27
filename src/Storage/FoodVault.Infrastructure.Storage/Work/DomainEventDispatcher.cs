using FoodVault.Infrastructure.Work;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Work
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        public Task DispatchEventsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
