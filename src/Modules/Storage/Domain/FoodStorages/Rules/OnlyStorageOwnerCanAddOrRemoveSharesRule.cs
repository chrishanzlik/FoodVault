using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;
using System;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Rules
{
    public class OnlyStorageOwnerCanAddOrRemoveSharesRule : IDomainRule
    {
        private readonly IUserContext _userContext;
        private readonly UserId _ownerId;

        public OnlyStorageOwnerCanAddOrRemoveSharesRule(UserId ownerId, IUserContext userContext)
        {
            _ownerId = ownerId;
            _userContext = userContext;
        }

        public string Message => throw new NotImplementedException();

        public bool Pass() => _userContext.UserId == _ownerId;
    }
}
