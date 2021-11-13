using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Models.Category;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetWalletCategories
{
    internal class GetWalletCategoriesQueryHandler : IRequestHandler<GetWalletCategoriesQuery, Result<IEnumerable<CategoryTreeResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CategoryTreeResponse>>> Handle(GetWalletCategoriesQuery query, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(query.WalletId, query.UserId, cancellationToken))
            {
                return new Result<IEnumerable<CategoryTreeResponse>>(new NotFoundError("Specified wallet doesn't exist or is not assigned to this user."));
            }

            var categories = await _unitOfWork.CategoriesRepository.GetWalletCategoriesForTreeViewAsync(query.WalletId, cancellationToken);
            var categoriesTree = GetCategoriesTree(categories, null);

            return new Result<IEnumerable<CategoryTreeResponse>>(categoriesTree);
        }

        private IEnumerable<CategoryTreeResponse> GetCategoriesTree(IEnumerable<CategoryForTreeViewModel> categories, int? parentCategoryId)
        {
            return categories
                .Where(x => x.ParentCategoryId == parentCategoryId)
                .Select(x => new CategoryTreeResponse(x.Id, x.Name, GetCategoriesTree(categories, x.Id)));
        }
    }
}