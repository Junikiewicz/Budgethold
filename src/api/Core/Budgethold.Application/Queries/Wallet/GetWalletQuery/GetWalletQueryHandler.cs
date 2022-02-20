using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Queries.Wallet.Common;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Wallet.GetWalletQuery
{
    public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, Result<WalletResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<WalletResponse>> Handle(GetWalletQuery query, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.WalletsRepository.IsWalletAssignedToUserAsync(query.WalletId, query.UserId, cancellationToken))
            {
                return new Result<WalletResponse>(new NotFoundError("Specified wallet doesn't exist or this user doesn't have access to it."));
            }

            var wallet = await _unitOfWork.WalletsRepository.GetWalletResponseAsync(query.WalletId, cancellationToken);

            return new Result<WalletResponse>(wallet);
        }
    }
}
