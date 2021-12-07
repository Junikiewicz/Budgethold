using Budgethold.Application.Queries.Transaction.GetSingleTransaction;
using Budgethold.Application.Queries.Transaction.GetWalletTransactions;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken);
        public Task<TransactionResponse?> GetSingleTransactionResponseAsync(int transactionId, CancellationToken cancellationToken);
        public Task<IEnumerable<TransactionResponse>> GetWalletTransactionsResponseAsync(int walletId, CancellationToken cancellationToken);
    }
}
