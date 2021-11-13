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
            Users = new HashSet<User>(userIds.Select(x => new User(x)));
            Categories = new();
        }

        public Wallet(string name, decimal startingValue, int userId, params int[] userIds)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            Users = new HashSet<User>(userIds.Select(x => new User(x)));
            Categories = new();
            OwningUserId = userId;
        }

        public int Id { get; private set; }
        public decimal StartingValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public string Name { get; private set; }
        public int OwningUserId { get; private set; }

        public virtual User OwningUser { get; private set; }
        public virtual HashSet<User> Users { get; private set; }
        public virtual HashSet<Category> Categories { get; private set; }

        public void Update(string name, int newOwner, decimal startingValue, params int[] userIds)
        {
            UpdateName(name);
            UpdateOwner(newOwner);
            UpdateStartingValue(startingValue);
            UpdateUsers(userIds);

        }
        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateOwner(int newOwner)
        {
            OwningUserId = newOwner;
        }
        public void UpdateStartingValue(decimal startingValue)
        {
            CurrentValue = CurrentValue - StartingValue + startingValue;
            StartingValue = startingValue;
        }
        public void UpdateUsers (params int[] userIds)
        {
            Users = new HashSet<User>(userIds.Select(x => new User(x)));
        }
    }
}
