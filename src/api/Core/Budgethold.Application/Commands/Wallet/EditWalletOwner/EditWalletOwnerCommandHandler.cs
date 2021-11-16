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
            var wallet = await _unitOfWork.WalletsRepository.GetWalletWithUserWalletsAsync(command.WalletId, cancellationToken);
            if (wallet is null || !await _unitOfWork.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(wallet.Id, command.NewOwnerId, cancellationToken)
               || !wallet.CheckIfUserIsWalletOwnerAsync(command.UserId))
            {
                return new Result(new NotFoundError("Specified wallet doesn't exist, this user doesn't have access to it or new User doesnt belong to this wallet"));
            }
            wallet.ChangeWalletOwner(command.NewOwnerId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
