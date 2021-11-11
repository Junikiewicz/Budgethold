using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletsQuery
{
    public class GetUserWalletsQuery : IRequest<Result<IEnumerable<WalletResponse>>>
    {
        public GetUserWalletsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
