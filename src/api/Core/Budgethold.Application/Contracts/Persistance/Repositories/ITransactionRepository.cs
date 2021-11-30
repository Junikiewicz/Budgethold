using Budgethold.Application.Queries.Transaction.GetSingleTransaction;
using Budgethold.Application.Queries.Transaction.GetUserTransactions;
using Budgethold.Domain.Common;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<Transaction?> GetTransaction(int transactionId, CancellationToken cancellationToken);
        public Task<TransactionResponse?> GetSingleTransaction(int transactionId, CancellationToken cancellationToken);
        public Task<TransactionsListResponse> GetWalletTransactionsList(int walletId, CancellationToken cancellationToken);
    }
}
