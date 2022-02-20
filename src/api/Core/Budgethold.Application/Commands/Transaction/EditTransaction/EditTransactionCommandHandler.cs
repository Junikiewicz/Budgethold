using Budgethold.Application.Commands.Transaction.Helpers;
using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;
using DomainModel = Budgethold.Domain.Models;

namespace Budgethold.Application.Commands.Transaction.EditTransaction
{
    public class EditTransactionCommandHandler : IRequestHandler<EditTransactionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetTransactionOrDefaultAsync(request.TransactionId, cancellationToken);

            if (transaction is null) return new Result(new NotFoundError("Specified transaction does not exist or is not assigned to this user."));

            if (request.CategoryId != transaction.CategoryId)
            {
                var newCategory = await _unitOfWork.CategoriesRepository.GetCategoryOrDefaultAsync(request.CategoryId, cancellationToken);

                if (newCategory is null || newCategory.UserId != request.UserId) return new Result(new NotFoundError("Specified transaction doesn't exist or is not assigned to this user"));
            }

            var updateWalletsResult = await UpdateWallets(request, transaction, cancellationToken);

            if (!updateWalletsResult.Succeeded) return updateWalletsResult;

            transaction.EditTransaction(request.Name, request.Description, request.Date, request.Amount, request.CategoryId, request.WalletId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }

        public async Task<Result> UpdateWallets(EditTransactionCommand request, DomainModel.Transaction transaction, CancellationToken cancellationToken)
        {
            var oldWallet = await _unitOfWork.WalletsRepository.GetWalletAsync(transaction.WalletId, cancellationToken);

            if (oldWallet.UserId != request.UserId) return new Result(new NotFoundError("Specified transaction doesn't exist or is not assigned to this user"));

            var oldCategoryTransactionType = await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(transaction.CategoryId, cancellationToken);

            var newCategoryTransactionType = request.CategoryId != transaction.CategoryId ? await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(request.CategoryId, cancellationToken) : oldCategoryTransactionType;

            var oldTransactionAmount = TransactionHelper.GetTransactionValue(oldCategoryTransactionType, transaction.Amount);

            var newTransactionAmount = TransactionHelper.GetTransactionValue(newCategoryTransactionType, request.Amount);

            if (request.WalletId != transaction.WalletId)
            {
                var newWallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(request.WalletId, cancellationToken);

                if (newWallet is null || newWallet.UserId != request.UserId) return new Result(new NotFoundError("Specified wallet does not exist or is not assigned to this user."));

                if (newWallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

                oldWallet.RevertTransactionValueChange(oldTransactionAmount);
                newWallet.ApplyNewTransaction(newTransactionAmount);
            }
            else
            {
                oldWallet.EditTransactionValueChange(oldTransactionAmount, newTransactionAmount);
            }

            return new Result();
        }
    }
}
