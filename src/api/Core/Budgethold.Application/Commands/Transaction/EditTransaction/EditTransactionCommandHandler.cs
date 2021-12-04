using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using Budgethold.Domain.Enums;
using MediatR;

namespace Budgethold.Application.Commands.Transaction.EditTransaction
{
    internal class EditTransactionCommandHandler : IRequestHandler<EditTransactionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetTransactionAsync(request.TransactionId, cancellationToken);

            if (transaction is null
                || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(transaction.WalletId, request.UserId, cancellationToken)
                || !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWalletAsync(request.CategoryId, transaction.WalletId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified transaction does not exist or is not assigned to this user."));
            }

            var wallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(request.WalletId, cancellationToken);

            if (wallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

            var oldCategoryTransactionType = await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(transaction.CategoryId, cancellationToken);

            var newCategoryTransactionType = request.CategoryId != transaction.CategoryId ? await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(request.CategoryId, cancellationToken) : oldCategoryTransactionType;

            decimal oldTransactionAmount = AmountSign.SetAmountSign(oldCategoryTransactionType, transaction.Amount);

            decimal newTransactionAmount = AmountSign.SetAmountSign(newCategoryTransactionType, request.Amount);

            wallet.EditTransactionValue(oldTransactionAmount, newTransactionAmount);

            transaction.EditTransaction(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
