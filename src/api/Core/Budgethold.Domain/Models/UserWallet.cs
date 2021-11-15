using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class UserWallet : Entity
    {
        public UserWallet(int userId)
        {
            UserId = userId;
            Wallet = null!;
            User = null!;
        }
        public UserWallet(int userId, int walletId)
        {
            UserId = userId;
            WalletId = walletId;
            Wallet = null!;
            User = null!;
        }

        public int UserId { get; private set; }
        public int WalletId { get; private set; }
        public bool IsOwner { get; private set; }

        public virtual Wallet Wallet { get; private set; }
        public virtual User User { get; private set; }

        public void SetOwnership()
        {
            IsOwner = true;
        }
        public void DeleteOwnership()
        {
            IsOwner = false;
        }
    }
}
