using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommandHandler : IRequestHandler<EditWalletCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditWalletCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditWalletCommand command, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.WalletsRepository.GetWalletOrDefaultAsync(command.WalletId, cancellationToken);

            if (wallet is null || wallet.UserId != command.UserId)
            {
                return new Result(new NotFoundError("Specified wallet doesn't exist or this user doesn't have access to it."));
            }

            wallet.Update(command.Name, command.StartingValue);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
