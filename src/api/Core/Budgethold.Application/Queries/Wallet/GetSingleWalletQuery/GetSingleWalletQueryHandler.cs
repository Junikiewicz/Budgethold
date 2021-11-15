using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetSingleWalletQuery
{
    public class GetSingleWalletQueryHandler : IRequestHandler<GetSingleWalletQuery, Result<SingleWalletResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleWalletQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SingleWalletResponse>> Handle(GetSingleWalletQuery query, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.WalletsRepository.GetUserWalletForViewAsync(query.WalletId, query.UserId, cancellationToken);
            if (wallet is null || !await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(query.WalletId, query.UserId, cancellationToken))
            {
                return new Result<SingleWalletResponse>(new NotFoundError("Specified wallet doesn't exist or this user doesn't have access to it."));
            }

            return new Result<SingleWalletResponse>(wallet!);
        }
    }
}
