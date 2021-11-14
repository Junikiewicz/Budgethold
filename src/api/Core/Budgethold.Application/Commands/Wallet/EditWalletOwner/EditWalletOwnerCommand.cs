using Budgethold.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Commands.Wallet.EditWalletOwner
{
    public class EditWalletOwnerCommand : IRequest<Result>
    {
        public EditWalletOwnerCommand(int newOwnerId, int userId, int walletId)
        {
            NewOwnerId = newOwnerId;
            WalletId = walletId;
            UserId = userId;
        }

        public int NewOwnerId { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
    }
}
