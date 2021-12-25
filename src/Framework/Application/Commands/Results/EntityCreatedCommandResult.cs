using System;
using System.Collections.Generic;

namespace FoodVault.Framework.Application.Commands.Results
{
    public class EntityCreatedCommandResult : ICommandResult
    {
        internal EntityCreatedCommandResult(Guid entityId)
        {
            EntityId = entityId;
            Success = Success;
        }

        public Guid EntityId { get; }

        public bool Success { get; }
    }
}
