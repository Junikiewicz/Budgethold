using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Queries.Transaction.Common;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetTransaction
{
    internal class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Result<TransactionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransactionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionResponse>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetTransactionResponseAsync(request.TransactionId, cancellationToken);

            if (transaction is null || !await _unitOfWork.WalletsRepository.IsWalletAssignedToUserAsync(transaction.WalletId, request.UserId, cancellationToken))
            {
                return new Result<TransactionResponse>(new NotFoundError("This transaction doesn't belong to this wallet or doesnt exist"));
            }

            return new Result<TransactionResponse>(transaction);
        }
    }
}
