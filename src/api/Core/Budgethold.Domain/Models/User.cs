using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class User : Entity
    {
        public User()
        {
            Name = null!;
            UserWallets = null!;
        }

        public User(int id)
        {
            Id = id;
            Name = null!;
            UserWallets = null!;
        }

        public User(string name)
        {
            Name = name;
            UserWallets = new ();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public virtual HashSet<UserWallet> UserWallets { get; private set; }
    }
}
