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
            var wallet = await _unitOfWork.WalletsRepository.GetUserWalletAsync(command.WalletId, command.UserId, cancellationToken);

            if (wallet is null || !await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(wallet.Id, command.UserId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified wallet doesn't exist or this user doesn't have access to it."));
            }

            wallet.Update(command.Name, command.StartingValue, command.UsersId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
