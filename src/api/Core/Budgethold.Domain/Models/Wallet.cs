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

        public Wallet(string name, decimal startingValue, IEnumerable<int> usersId)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            UserWallets = new HashSet<UserWallet>(usersId.Select(x => new UserWallet(x)));
            Categories = new();
        }

        public Wallet(string name, decimal startingValue, int userId, IEnumerable<int> usersId)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            UserWallets = new HashSet<UserWallet>(usersId.Select(x => new UserWallet(x)));
            SetWalletOwner(userId);
            Categories = new();
        }

        public int Id { get; private set; }
        public decimal StartingValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public string Name { get; private set; }
        public virtual HashSet<UserWallet> UserWallets { get; private set; }
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
        public void SetWalletOwner(int newOwnerId)
        {
            UserWallets.SingleOrDefault(x => x.UserId == newOwnerId)!.SetOwnership();
        }
        public void RemoveWalletOwner(int newOwnerId)
        {

            UserWallets.SingleOrDefault(x => x.IsOwner == true)!.DeleteOwnership();
            SetWalletOwner(newOwnerId);
        }
    }
}
