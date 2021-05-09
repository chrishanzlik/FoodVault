using FoodVault.Framework.Application.Outbox;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Outbox
{
    /// <summary>
    /// Stores informations that should processed outside of a transaction.
    /// </summary>
    internal class OutboxAccessor : IOutbox
    {
        private UserAccessContext _userAccessContext;

        /// <inheritdoc />
        public OutboxAccessor(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        /// <inheritdoc />
        public void Add(OutboxMessage message)
        {
            _userAccessContext.OutboxMessages.Add(message);
        }

        /// <inheritdoc />
        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}
