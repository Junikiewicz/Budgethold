using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class Wallet : Entity
    {
        public Wallet()
        {
            Users = null!;
            Name = null!;
            Categories = null!;
        }

        public Wallet(string name, decimal startingValue, params int[] userIds)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            Users = new HashSet<UserWallet>(userIds.Select(x => new UserWallet(x)));
            Categories = new();
        }

        public Wallet(string name, decimal startingValue, int userId, params int[] userIds)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            Users = new HashSet<UserWallet>(userIds.Select(x => new UserWallet(x)));
            Categories = new();
        }

        public int Id { get; private set; }
        public decimal StartingValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public string Name { get; private set; }
        public virtual HashSet<UserWallet> Users { get; private set; }
        public virtual HashSet<Category> Categories { get; private set; }

        public void Update(string name, decimal startingValue)
        {
            UpdateName(name);
            UpdateStartingValue(startingValue);
        }
        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateStartingValue(decimal startingValue)
        {
            CurrentValue = CurrentValue - StartingValue + startingValue;
            StartingValue = startingValue;
        }
    }
}
