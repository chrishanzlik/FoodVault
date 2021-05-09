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
            Errors = new List<string>();
        }

        public Guid EntityId { get; }

        public bool Success { get; }

        public IEnumerable<string> Errors { get; }
    }
}
