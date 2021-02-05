using System.Threading.Tasks;

namespace FoodVault.Framework.Application.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);

        Task SaveAsync();
    }
}
