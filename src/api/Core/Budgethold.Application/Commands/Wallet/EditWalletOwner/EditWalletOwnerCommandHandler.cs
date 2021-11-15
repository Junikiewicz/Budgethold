using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.EditWalletOwner
{
    public class EditWalletOwnerCommandHandler : IRequestHandler<EditWalletOwnerCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditWalletOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditWalletOwnerCommand command, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.WalletsRepository.GetUserWalletAsync(command.WalletId, command.UserId, cancellationToken);
            if (wallet is null || !await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(wallet.Id, command.NewOwnerId, cancellationToken)
               || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsWalletOwnerAsync(wallet.Id, command.UserId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified wallet doesn't exist or this user doesn't have access to it."));
            }
            //wallet.UserWallets.SingleOrDefault(x => x.UserId == command.UserId && x.WalletId == command.WalletId)!.DeleteOwnership();
            //wallet.UserWallets.SingleOrDefault(x => x.UserId == command.NewOwnerId && x.WalletId == command.WalletId)!.SetOwnership();
            wallet.RemoveWalletOwner(command.NewOwnerId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
