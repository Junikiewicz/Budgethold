using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Queries.Transaction.Common;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context)
        {
        }

        public async Task<Transaction?> GetTransactionOrDefaultAsync(int transactionId, CancellationToken cancellationToken)
        {
            return await Context.Transactions.Where(x => x.Id == transactionId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TransactionResponse> GetTransactionResponseAsync(int transactionId, CancellationToken cancellationToken)
        {
            return await Context.Transactions.Where(x => x.Id == transactionId).Select(x => new TransactionResponse
            {
                Amount = x.Amount,
                Description = x.Description,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Date = x.Date,
                Id = x.Id,
                WalletId = x.WalletId
            }).SingleAsync(cancellationToken);
        }

        public async Task<IEnumerable<TransactionResponse>> GetUserTransactionsResponseAsync(int userId, CancellationToken cancellationToken)
        {
            return await Context.Transactions.Where(x => x.Category.UserId == userId).Select(x => new TransactionResponse
            {
                Amount = x.Amount,
                Description = x.Description,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Date = x.Date,
                Id = x.Id,
                WalletId = x.WalletId
            }).ToListAsync(cancellationToken);
        }
    }
}
