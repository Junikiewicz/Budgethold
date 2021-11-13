using Budgethold.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommand : IRequest<Result>
    {
        public EditWalletCommand(int walletId, string name, decimal? startingValue, int userId, int? newOwner, int[] users)
        {
            WalletId = walletId;
            Users = users;
            OwnerId = newOwner;
            Name = name;
            StartingValue = startingValue;
            UserId = userId;
        }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int[] Users { get; set; }
        public int? OwnerId { get; set; }
        public string Name { get; set; }
        public decimal? StartingValue { get; set; }
    }
}

