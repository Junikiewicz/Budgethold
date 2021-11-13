using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class Wallet : IEntity
    {
        public Wallet()
        {
            Users = null!;
            Name = null!;
        }

        public Wallet(string name, decimal startingValue, int ownerId, params int[] users)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            Users = new HashSet<User>(users.Select(x => new User { Id = x }));
            OwningUserId = ownerId;
        }

        public int Id { get; set; }
        public int OwningUserId { get; set; }
        public decimal StartingValue { get; set; }
        public decimal CurrentValue { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public HashSet<User> Users { get; set; }
        public User OwningUser { get; set; }
    }
}
