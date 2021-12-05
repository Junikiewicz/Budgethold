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

            if (request.WalletId is not null)
            {
                if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(request.WalletId.Value, request.UserId, cancellationToken)
                    || request.CategoryId is not null && !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWalletAsync(request.CategoryId.Value, request.WalletId.Value, cancellationToken))
                    return new Result(new NotFoundError("Specified category does not exist or is not assigned to this user."));
            }
            else
            {
                if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(transaction.WalletId, request.UserId, cancellationToken)
                    || request.CategoryId is not null && !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWalletAsync(request.CategoryId.Value, transaction.WalletId, cancellationToken))
                    return new Result(new NotFoundError("Specified category does not exist or is not assigned to this user."));
            }

            var currentWallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(transaction.WalletId, cancellationToken);

            if (currentWallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

            var oldCategoryTransactionType = await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(transaction.CategoryId, cancellationToken);

            var newCategoryTransactionType = request.CategoryId is not null && (request.CategoryId != transaction.CategoryId) ? await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(request.CategoryId.Value, cancellationToken) : oldCategoryTransactionType;

            decimal oldTransactionAmount = ITransactionHelper.SetAmountSign(oldCategoryTransactionType, transaction.Amount);

            decimal newTransactionAmount = ITransactionHelper.SetAmountSign(newCategoryTransactionType, request.Amount);

            if ((request.WalletId != transaction.WalletId) && request.WalletId is not null)
            {
                var newWallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(request.WalletId.Value, cancellationToken);

                if (newWallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

                currentWallet.ApplyTransactionValueChange(oldTransactionAmount, 0);
                newWallet.ApplyTransactionValueChange(0, newTransactionAmount);
            }
            else
            {
                currentWallet.ApplyTransactionValueChange(oldTransactionAmount, newTransactionAmount);
            }

            transaction.EditTransaction(request.Name, request.Description, request.Date, request.Amount, request.CategoryId, request.WalletId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
