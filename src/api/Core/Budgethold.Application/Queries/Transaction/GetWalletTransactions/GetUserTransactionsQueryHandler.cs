using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Queries.Transaction.Common;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    internal class GetUserTransactionsQueryHandler : IRequestHandler<GetUserTransactionsQuery, Result<IEnumerable<TransactionResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<TransactionResponse>>> Handle(GetUserTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _unitOfWork.TransactionRepository.GetUserTransactionsResponseAsync(request.UserId, cancellationToken);

            return new Result<IEnumerable<TransactionResponse>>(transactions);
        }
    }
}
