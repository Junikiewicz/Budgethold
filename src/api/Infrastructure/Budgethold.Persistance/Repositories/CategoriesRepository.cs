using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Models.Category;
using Budgethold.Application.Queries.Category.GetWalletCategory;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(DataContext context) : base(context) { }

        public async Task<bool> CheckIfCategoryBelongsToWalletAsync(int categoryId, int walletId, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .AnyAsync(x => x.Id == categoryId && x.WalletId == walletId, cancellationToken);

            return category;
        }

        public async Task<CategoryForOwnershipVerificationModel?> GetCategoryForOwnershipVerificationAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(x => x.Id == categoryId)
                .Select(x => new CategoryForOwnershipVerificationModel(x.TransactionTypeId, x.WalletId))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Category?> GetCategoryOrDefaultAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        }

        public async Task<int> GetCategoryTransactionTypeAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Categories.Where(x => x.Id == categoryId)
                .Select(x => x.TransactionTypeId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Category?> GetCategoryWithChildrensOrDefaultAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Categories.Include(x => x.ChildCategories)
                .SingleOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        }

        public async Task<CategoryResponse?> GetSingleCategoryResponseAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(x => x.Id == categoryId)
                .Select(x => new CategoryResponse(x.Id, x.Name, x.ParentCategoryId, x.TransactionTypeId, x.WalletId))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<CategoryForTreeViewModel>> GetWalletCategoriesForTreeViewAsync(int walletId, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(x => x.WalletId == walletId)
                .Select(x => new CategoryForTreeViewModel(x.Id, x.ParentCategoryId, x.Name))
                .ToListAsync(cancellationToken);
        }
    }
}
