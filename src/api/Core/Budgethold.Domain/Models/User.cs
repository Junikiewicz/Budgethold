using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class User : Entity
    {
        public User()
        {
            Name = null!;
            Wallets = null!;
            Categories = null!;
        }

        public User(int id)
        {
            Id = id;
            Name = null!;
            Wallets = null!;
            Categories = null!;
        }

        public User(string name)
        {
            Name = name;
            Wallets = new ();
            Categories = new ();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public virtual HashSet<Wallet> Wallets { get; private set; }

        public virtual HashSet<Category> Categories { get; private set; }
    }
}
