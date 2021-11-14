using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;
using DomainModel = Budgethold.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            wallet.Users.RemoveWhere(x => !command.Users.Contains(x.UserId));
            var currentUsers = wallet.Users.Select(x => x.UserId);
            var usersToAdd = command.Users.Except(currentUsers);
            var newUsers = usersToAdd.Select(x => new UserWallet(x, wallet.Id));
            foreach (var user in newUsers)
            {
                wallet.Users.Add(user);
            }
            wallet.Update(command.Name, command.StartingValue);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result();
        }
    }
}
