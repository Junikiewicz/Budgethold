using Budgethold.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletQuery
{
    public class GetUserWalletQuery : IRequest<Result<WalletResponse>>
    {
        public GetUserWalletQuery(int walletId, int userId)
        {
            WalletId = walletId;
            UserId = userId;
        }

        public int WalletId { get; set; }
        public int UserId { get; set; }
    }
}
