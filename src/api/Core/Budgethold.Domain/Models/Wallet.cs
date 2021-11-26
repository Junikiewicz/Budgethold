﻿using Budgethold.Common.Extensions;
using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class Wallet : Entity
    {
        public Wallet()
        {
            UserWallets = null!;
            Name = null!;
            Categories = null!;
        }

        public Wallet(string name, decimal startingValue, int userId, IEnumerable<int> userIds)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            UserWallets = new HashSet<UserWallet>(userIds.Select(x => new UserWallet(x)));
            AddUserToWallet(userId);
            ChangeWalletOwner(userId);
            Categories = new();
        }

        public int Id { get; private set; }
        public decimal StartingValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public string Name { get; private set; }
        public virtual HashSet<UserWallet> UserWallets { get; private set; }
        public virtual HashSet<Category> Categories { get; private set; }

        public void Update(string name, decimal startingValue, IEnumerable<int> userIds)
        {
            Name = name;
            CurrentValue = CurrentValue - StartingValue + startingValue;
            StartingValue = startingValue;
            UpdateUsers(userIds);
        }

        public void UpdateUsers(IEnumerable<int> userIds)
        {
            var currentUsersId = UserWallets.Select(x => x.UserId);
            var newUsersId = userIds.Except(currentUsersId);
            var usersToAdd = newUsersId.Select(x => new UserWallet(x, Id));

            UserWallets.RemoveWhere(x => !userIds.Contains(x.UserId) && !x.IsOwner);
            UserWallets.AddRange(usersToAdd);
        }

        public void ChangeWalletOwner(int newOwnerId)
        {
            foreach (var userWallet in UserWallets)
            {
                userWallet.SetOwnership(userWallet.UserId == newOwnerId);
            }
        }

        public void AddUserToWallet(int userId)
        {
            if (!UserWallets.Select(x => x.UserId).Contains(userId)) UserWallets.Add(new UserWallet(userId, Id));
        }

        public bool CheckIfUserIsWalletOwnerAsync(int userId)
        {
            var user = UserWallets.SingleOrDefault(x => x.UserId == userId);
            return user is null ? false : user.IsOwner;
        }
    }
}
