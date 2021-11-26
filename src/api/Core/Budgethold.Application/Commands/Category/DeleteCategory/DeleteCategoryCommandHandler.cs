using Budgethold.Application.Contracts.Persistance;
using Budgethold.Common.Extensions;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Commands.Category.DeleteCategory
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoriesRepository.GetCategoryWithChildrensOrDefaultAsync(command.CategoryId, cancellationToken);

            if (category is null || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(category.WalletId, command.UserId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified category doesn't exist or this user doesn't have access to it."));
            }

            if (category.ChildCategories.Any())
            {
                category.ChildCategories.ForEach(x => x.UpdateParentCategoryId(category.ParentCategoryId));
            }

            _unitOfWork.CategoriesRepository.Remove(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
