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

        public int Id { get; private set; }
        public decimal StartingValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public string Name { get; private set; }

        public virtual HashSet<User> Users { get; private set; }
        public virtual HashSet<Category> Categories { get; private set; }
    }
}
