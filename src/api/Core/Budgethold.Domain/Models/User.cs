using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class User : IEntity
    {
        public User()
        {
            Name = null!;
            Wallets = new();
        }

        public User(string name)
        {
            Name = name;
            Wallets = new();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public HashSet<Wallet> Wallets { get; set; }
    }
}
