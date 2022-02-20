using Budgethold.Application.Queries.Transaction.Common;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<Transaction?> GetTransactionOrDefaultAsync(int transactionId, CancellationToken cancellationToken);

        public Task<TransactionResponse> GetTransactionResponseAsync(int transactionId, CancellationToken cancellationToken);

        public Task<IEnumerable<TransactionResponse>> GetUserTransactionsResponseAsync(int userId, CancellationToken cancellationToken);
    }
}
