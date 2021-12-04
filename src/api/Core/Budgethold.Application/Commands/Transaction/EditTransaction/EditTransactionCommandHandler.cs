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
            var transaction = await _unitOfWork.TransactionRepository.GetTransaction(request.TransactionId, cancellationToken);

            if (transaction is null
                || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(transaction.WalletId, request.UserId, cancellationToken)
                || !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWallet(request.CategoryId, transaction.WalletId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified transaction does not exist or is not assigned to this user."));
            }

            transaction.EditTransaction(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
