using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetUserWallets
{
    internal class GetUserWalletsQueryHandler : IRequestHandler<GetUserWalletsQuery, Result<IEnumerable<WalletResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserWalletsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<WalletResponse>>> Handle(GetUserWalletsQuery query, CancellationToken cancellationToken)
        {
            var wallets = await _unitOfWork.WalletsRepository.GetUserWalletsAsync(query.UserId, cancellationToken);

            return new Result<IEnumerable<WalletResponse>>(wallets);
        }
    }
}