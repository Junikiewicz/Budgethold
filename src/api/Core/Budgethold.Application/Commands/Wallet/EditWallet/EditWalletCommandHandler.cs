using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;
using Budgethold.Domain.Common.Errors;
using Budgethold.Domain.Models;

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

            if( wallet is null || !await _unitOfWork.WalletsRepository.CheckIfUserIsAssignedToWalletAsync(wallet.Id, command.UserId, cancellationToken))
            {
                return new Result(new NotFoundError("Specified wallet doesn't exist or this user doesn't have access to it."));
            }
            wallet.UserWallets.RemoveWhere(x => !command.UsersId.Contains(x.UserId));
            var currentUsersIdList = wallet.UserWallets.Select(x => x.UserId);
            var usersToAdd = command.UsersId.Except(currentUsersIdList);
            var newUsers = usersToAdd.Select(x => new UserWallet(x, wallet.Id));
            foreach (var user in newUsers)
            {
                wallet.UserWallets.Add(user);
            }
            wallet.Update(command.Name, command.StartingValue);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
