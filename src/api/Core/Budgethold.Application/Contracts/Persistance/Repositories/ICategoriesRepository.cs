using Budgethold.Application.Models.Category;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<CategoryForOwnershipVerificationModel?> GetCategoryForOwnershipVerificationAsync(int categoryId, CancellationToken cancellationToken);
        Task<Category?> GetCategoryOrDefaultAsync(int categoryId, CancellationToken cancellationToken);
        Task<Category?> GetCategoryWithChildrensOrDefaultAsync(int categoryId, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryForTreeViewModel>> GetWalletCategoriesForTreeViewAsync(int walletId, CancellationToken cancellationToken);
    }
}
