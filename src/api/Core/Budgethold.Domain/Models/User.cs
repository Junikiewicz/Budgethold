using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class User : Entity
    {
        public User()
        {
            Name = null!;
            Wallets = null!;
        }

        public User(int id)
        {
            Id = id;
            Name = null!;
            Wallets = null!;
        }

        public User(string name)
        {
            Name = name;
            Wallets = new();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public virtual HashSet<UserWallet> Wallets { get; private set; }
    }
}
