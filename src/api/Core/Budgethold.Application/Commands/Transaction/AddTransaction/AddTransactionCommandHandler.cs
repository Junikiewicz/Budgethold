using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
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
            if (!await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(request.WalletId, request.UserId, cancellationToken) ||
                !await _unitOfWork.CategoriesRepository.CheckIfCategoryBelongsToWallet(request.CategoryId, request.WalletId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified category or wallet doesn't exist or is not assigned to this user."));
            }

            var transaction = new DomainModel.Transaction(request.Name, request.Description, request.WalletId, request.CategoryId, request.Amount);

            _unitOfWork.TransactionRepository.Add(transaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<int>(transaction.Id);
        }
    }
}
