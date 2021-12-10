using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Queries.Category.GetWalletCategory
{
    internal class GetWalletCategoryQueryHandler : IRequestHandler<GetWalletCategoryQuery, Result<CategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CategoryResponse>> Handle(GetWalletCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoriesRepository.GetSingleCategoryResponseAsync(request.CategoryId, cancellationToken);

            if (category is null || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(category.WalletId, request.UserId, cancellationToken))
            {
                return new Result<CategoryResponse>(new NotFoundError("Specified category doesn't exist or is not assigned to this user."));
            }

            return new Result<CategoryResponse>(category);
        }
    }
}
