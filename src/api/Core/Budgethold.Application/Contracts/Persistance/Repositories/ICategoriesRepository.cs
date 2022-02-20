using Budgethold.Application.Models.Category;
using Budgethold.Application.Queries.Category.GetCategory;
using Budgethold.Domain.Models;

namespace Budgethold.Application.Contracts.Persistance.Repositories
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<CategoryForOwnershipVerificationModel?> GetCategoryForOwnershipVerificationAsync(int categoryId, CancellationToken cancellationToken);

        Task<Category?> GetCategoryOrDefaultAsync(int categoryId, CancellationToken cancellationToken);

        Task<Category?> GetCategoryWithChildrensOrDefaultAsync(int categoryId, CancellationToken cancellationToken);

        Task<IEnumerable<CategoryForTreeViewModel>> GetUserCategoriesForTreeViewAsync(int userId, CancellationToken cancellationToken);

        Task<int> GetCategoryTransactionTypeAsync(int categoryId, CancellationToken cancellationToken);

        Task<CategoryResponse> GetCategoryResponseAsync(int categoryId, CancellationToken cancellationToken);

        Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken);

        Task<bool> IsCategoryAssignedToUserAsync(int categoryId, int userId, CancellationToken cancellationToken);
    }
}
