using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletQuery
{
    public class GetUserWalletQuery : IRequest<Result<SingleWalletResponse>>
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
