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
            if (command.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.CategoriesRepository.GetCategoryForOwnershipVerificationAsync(command.ParentCategoryId.Value, cancellationToken);

                if (parentCategory is null || parentCategory.UserId != command.UserId)
                    return new Result(new NotFoundError("Specified parent category doesn't exist or is not assigned to this wallet."));

                if (parentCategory.TransactionTypeId != command.TransactionTypeId)
                    return new Result(new InvalidOperationError("Specified parent category has different transaction type."));
            }

            var category = new DomainModel.Category(command.Name, command.ParentCategoryId, command.TransactionTypeId, command.UserId);

            _unitOfWork.CategoriesRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedResult<int>(category.Id);
        }
    }
}
