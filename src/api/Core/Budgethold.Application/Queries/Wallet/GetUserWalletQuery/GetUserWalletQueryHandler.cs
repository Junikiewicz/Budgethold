using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletQuery
{
    public class GetUserWalletQueryHandler : IRequestHandler<GetUserWalletQuery, Result<SingleWalletResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserWalletQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SingleWalletResponse>> Handle(GetUserWalletQuery query, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.WalletsRepository.GetUserWalletForViewAsync(query.WalletId, query.UserId, cancellationToken);
            if (wallet is null || !await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(query.WalletId, query.UserId, cancellationToken);

            return new Result<SingleWalletResponse>(wallet!);
        }
    }
}
