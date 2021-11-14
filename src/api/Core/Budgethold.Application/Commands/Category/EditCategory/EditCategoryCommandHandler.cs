using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Commands.Category.EditCategory
{
    internal class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoriesRepository.GetCategoryOrDefaultAsync(command.CategoryId, cancellationToken);

            if (category is null || !await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(category.WalletId, command.UserId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified category doesn't exist or this user doesn't have access to it."));
            }

            if (command.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.CategoriesRepository.GetCategoryForOwnershipVerificationAsync(command.ParentCategoryId.Value, cancellationToken);

                if (parentCategory is null || parentCategory.WalletId != category.WalletId)
                    return new Result(new NotFoundError("Specified parent category doesn't exist or is not assigned to this wallet."));

                if (parentCategory.TransactionTypeId != category.TransactionTypeId)
                    return new Result(new InvalidOperationError("Specified parent category has different transaction type."));
            }

            category.Update(command.ParentCategoryId, command.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
