using Budgethold.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Domain.Models
{
    public class UserWallet : Entity
    {
        public UserWallet(int userId)
        {
            UserId = userId;
        }
        public UserWallet(int userId, int walletId)
        {
            UserId = userId;
            WalletId = walletId;
        }

        public int UserId { get; private set; }
        public User User { get; private set; }
        public int WalletId { get;  set; }
        public Wallet Wallet { get;  set; }
        public bool IsOwner { get; private set; }

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
