using Budgethold.Application.Queries.Transaction.GetSingleTransaction;

namespace Budgethold.Application.Queries.Transaction.GetWalletTransactions
{
    public record TransactionsListResponse
    {
        public TransactionsListResponse(IEnumerable<TransactionResponse> transactionResponses)
        {
            TransactionResponses = transactionResponses;
        }

        public IEnumerable<TransactionResponse> TransactionResponses { get; init; }
    }
}
