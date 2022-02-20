using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetCategory
{
    internal class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<CategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.CategoriesRepository.IsCategoryAssignedToUserAsync(request.CategoryId, request.UserId, cancellationToken))
            {
                return new Result<CategoryResponse>(new NotFoundError("Specified category doesn't exist or is not assigned to this user."));
            }

            var category = await _unitOfWork.CategoriesRepository.GetCategoryResponseAsync(request.CategoryId, cancellationToken);

            return new Result<CategoryResponse>(category);
        }
    }
}
