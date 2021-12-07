using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Queries.Transaction.GetSingleTransaction;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    internal class GetWalletTransactionsQueryHandler : IRequestHandler<GetWalletTransactionsQuery, Result<IEnumerable<TransactionResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<TransactionResponse>>> Handle(GetWalletTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(request.WalletId, request.UserId, cancellationToken))
                return new Result<IEnumerable<TransactionResponse>>(new NotFoundError("This wallet doesn't belong to this user"));

            var transactions = await _unitOfWork.TransactionRepository.GetWalletTransactionsResponseAsync(request.WalletId, cancellationToken);

            return new Result<IEnumerable<TransactionResponse>>(transactions);
        }
    }
}
