using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Models.Category;
using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetUserCategories
{
    internal class GetUserCategoriesQueryHandler : IRequestHandler<GetUserCategoriesQuery, Result<IEnumerable<CategoryTreeResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CategoryTreeResponse>>> Handle(GetUserCategoriesQuery query, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoriesRepository.GetUserCategoriesForTreeViewAsync(query.UserId, cancellationToken);
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
