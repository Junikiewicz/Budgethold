using Budgethold.Application.Commands.Transaction.Helpers;
using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
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

            if (transaction is null)
            {
                return new Result(new NotFoundError("Specified transaction does not exist or is not assigned to this user."));
            }

            if (request.WalletId != transaction.WalletId)
            {
                if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(transaction.WalletId, request.UserId, cancellationToken)
                    || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(request.WalletId, request.UserId, cancellationToken)
                    || request.CategoryId != transaction.CategoryId && !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWalletAsync(request.CategoryId, request.WalletId, cancellationToken))
                    return new Result(new NotFoundError("Specified category does not exist or is not assigned to this user."));
            }
            else
            {
                if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(transaction.WalletId, request.UserId, cancellationToken)
                    || request.CategoryId != transaction.CategoryId && !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWalletAsync(request.CategoryId, transaction.WalletId, cancellationToken))
                    return new Result(new NotFoundError("Specified category does not exist or is not assigned to this user."));
            }

            var currentWallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(transaction.WalletId, cancellationToken);

            if (currentWallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

            var oldCategoryTransactionType = await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(transaction.CategoryId, cancellationToken);

            var newCategoryTransactionType = request.CategoryId != transaction.CategoryId ? await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(request.CategoryId, cancellationToken) : oldCategoryTransactionType;

            decimal oldTransactionAmount = TransactionHelper.GetTransactionValue(oldCategoryTransactionType, transaction.Amount);

            decimal newTransactionAmount = TransactionHelper.GetTransactionValue(newCategoryTransactionType, request.Amount);

            if (request.WalletId != transaction.WalletId)
            {
                var newWallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(request.WalletId, cancellationToken);

                if (newWallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

                currentWallet.RevertTransactionValueChange(oldTransactionAmount);
                newWallet.ApplyNewTransaction(newTransactionAmount);
            }
            else
            {
                currentWallet.EditTransactionValueChange(oldTransactionAmount, newTransactionAmount);
            }

            transaction.EditTransaction(request.Name, request.Description, request.Date, request.Amount, request.CategoryId, request.WalletId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
