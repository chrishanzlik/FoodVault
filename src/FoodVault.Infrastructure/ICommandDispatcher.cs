using System;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
