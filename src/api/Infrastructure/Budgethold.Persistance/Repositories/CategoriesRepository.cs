using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Application.Models.Category;
using Budgethold.Application.Queries.Category.GetCategory;
using Budgethold.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budgethold.Persistance.Repositories
{
    internal class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(DataContext context) : base(context) { }

        public async Task<CategoryForOwnershipVerificationModel?> GetCategoryForOwnershipVerificationAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await Context.Categories
                .Where(x => x.Id == categoryId)
                .Select(x => new CategoryForOwnershipVerificationModel(x.TransactionTypeId, x.UserId))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await Context.Categories.SingleAsync(x => x.Id == categoryId, cancellationToken);
        }

        public async Task<Category?> GetCategoryOrDefaultAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await Context.Categories.SingleOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        }

        public async Task<int> GetCategoryTransactionTypeAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await Context.Categories.Where(x => x.Id == categoryId)
                .Select(x => x.TransactionTypeId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Category?> GetCategoryWithChildrensOrDefaultAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await Context.Categories.Include(x => x.ChildCategories)
                .SingleOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        }

        public async Task<CategoryResponse> GetCategoryResponseAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await Context.Categories
                .Where(x => x.Id == categoryId)
                .Select(x => new CategoryResponse(x.Id, x.Name, x.ParentCategoryId, x.TransactionTypeId))
                .SingleAsync(cancellationToken);
        }

        public async Task<IEnumerable<CategoryForTreeViewModel>> GetUserCategoriesForTreeViewAsync(int userId, CancellationToken cancellationToken)
        {
            return await Context.Categories
                .Where(x => x.UserId == userId)
                .Select(x => new CategoryForTreeViewModel(x.Id, x.ParentCategoryId, x.Name))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsCategoryAssignedToUserAsync(int categoryId, int userId, CancellationToken cancellationToken)
        {
            return await Context.Categories.AnyAsync(x => x.Id == categoryId && x.UserId == userId, cancellationToken);
        }
    }
}
