using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetSingleTransaction
{
    internal class GetSingleTransactionQueryHandler : IRequestHandler<GetSingleTransactionQuery, Result<TransactionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleTransactionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionResponse>> Handle(GetSingleTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetSingleTransactionResponseAsync(request.TransactionId, cancellationToken);

            if (transaction is null
                || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(transaction.WalletId, request.UserId, cancellationToken))
                return new Result<TransactionResponse>(new NotFoundError("This transaction doesn't belong to this wallet or doesnt exist"));

            return new Result<TransactionResponse>(transaction);
        }
    }
}
