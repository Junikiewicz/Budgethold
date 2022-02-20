using Budgethold.Application.Commands.Transaction.Helpers;
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
            var wallet = await _unitOfWork.WalletsRepository.GetWalletAsync(request.WalletId, cancellationToken);

            if (wallet is null || wallet.UserId != request.UserId) return new Result(new NotFoundError("Specified wallet doesn't exist or is not assigned to this user"));

            var category = await _unitOfWork.CategoriesRepository.GetCategoryOrDefaultAsync(request.CategoryId, cancellationToken);

            if (category is null || category.UserId != request.UserId) return new Result(new NotFoundError("Specified category doesn't exist or is not assigned to this user"));

            var transaction = new DomainModel.Transaction(request.Name, request.Description, request.WalletId, request.CategoryId, request.Amount, request.Date);

            wallet.ApplyNewTransaction(TransactionHelper.GetTransactionValue(category.TransactionTypeId, transaction.Amount));

            _unitOfWork.TransactionRepository.Add(transaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedResult<int>(transaction.Id);
        }
    }
}
