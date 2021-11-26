using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;
using DomainModel = Budgethold.Domain.Models;

namespace Budgethold.Application.Commands.Category.AddCategory
{
    internal class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(command.WalletId, command.UserId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified wallet doesn't exist or is not assigned to this user."));
            }

            if (command.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.CategoriesRepository.GetCategoryForOwnershipVerificationAsync(command.ParentCategoryId.Value, cancellationToken);

                if (parentCategory is null || parentCategory.WalletId != command.WalletId)
                    return new Result(new NotFoundError("Specified parent category doesn't exist or is not assigned to this wallet."));

                if (parentCategory.TransactionTypeId != command.TransactionTypeId)
                    return new Result(new InvalidOperationError("Specified parent category has different transaction type."));
            }

            var category = new DomainModel.Category(command.Name, command.ParentCategoryId, command.TransactionTypeId, command.WalletId);

            _unitOfWork.CategoriesRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<int>(category.Id);
        }
    }
}
