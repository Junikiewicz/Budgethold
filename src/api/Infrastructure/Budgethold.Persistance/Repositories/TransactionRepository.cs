using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Queries.Transaction.GetSingleTransaction;
using Budgethold.Application.Queries.Transaction.GetWalletTransactions;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context)
        {
        }

        public async Task<TransactionResponse?> GetSingleTransactionResponseAsync(int transactionId, CancellationToken cancellationToken)
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
            }).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken)
        {
            return await Context.Transactions.Where(x => x.Id == transactionId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TransactionResponse>> GetWalletTransactionsResponseAsync(int walletId, CancellationToken cancellationToken)
        {
            return await Context.Transactions.Where(x => x.WalletId == walletId).Select(x => new TransactionResponse
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
