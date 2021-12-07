using Budgethold.Application.Commands.Transaction.Helpers;
using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using Budgethold.Domain.Enums;
using MediatR;
using DomainModel = Budgethold.Domain.Models;

namespace Budgethold.Application.Commands.Transaction.AddTransaction
{
    internal class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(request.WalletId, request.UserId, cancellationToken)
                || !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWalletAsync(request.CategoryId, request.WalletId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified category or wallet doesn't exist or is not assigned to this user."));
            }

            var transaction = new DomainModel.Transaction(request.Name, request.Description, request.WalletId, request.CategoryId, request.Amount);

            var wallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(request.WalletId, cancellationToken);

            if (wallet is null) return new Result(new NotFoundError("Specified wallet doesn't exist"));

            var categoryTransactionType = await _unitOfWork.CategoriesRepository
                .GetCategoryTransactionTypeAsync(request.CategoryId, cancellationToken);

            var transactionAmount = TransactionHelper.GetTransactionValue(categoryTransactionType, transaction.Amount);

            wallet.ApplyNewTransaction(transactionAmount);

            _unitOfWork.TransactionRepository.Add(transaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<int>(transaction.Id);
        }
    }
}
