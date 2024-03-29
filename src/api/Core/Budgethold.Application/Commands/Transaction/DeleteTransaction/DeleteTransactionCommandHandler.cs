﻿using Budgethold.Application.Commands.Transaction.Helpers;
using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Commands.Transaction.DeleteTransaction
{
    internal class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetTransactionOrDefaultAsync(request.TransactionId, cancellationToken);

            if (transaction is null)
            {
                return new Result(new NotFoundError("Specified transaction doesn't exist or is not created by this user."));
            }

            var wallet = await _unitOfWork.WalletsRepository.GetWalletAsync(transaction.WalletId, cancellationToken);

            if (wallet.UserId != request.UserId) return new Result(new NotFoundError("Specified transaction doesn't exist or is not created by this user."));

            var categoryTransactionType = await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(transaction.CategoryId, cancellationToken);

            var transactionAmount = TransactionHelper.GetTransactionValue(categoryTransactionType, transaction.Amount);

            wallet.RevertTransactionValueChange(transactionAmount);

            _unitOfWork.TransactionRepository.Remove(transaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
