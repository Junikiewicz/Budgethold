using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    internal class GetWalletTransactionsQueryHandler : IRequestHandler<GetWalletTransactionsQuery, Result<TransactionsListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<TransactionsListResponse>> Handle(GetWalletTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(request.WalletId, request.UserId, cancellationToken))
                return new Result<TransactionsListResponse>(new NotFoundError("This wallet doesn't belong to this user"));

            var transactions = await _unitOfWork.TransactionRepository.GetWalletTransactionsList(request.WalletId, cancellationToken);

            return new Result<TransactionsListResponse>(transactions);


        }
    }
}
