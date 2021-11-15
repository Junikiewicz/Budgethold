using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;
using DomainModel = Budgethold.Domain.Models;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    internal class AddWalletCommandHandler : IRequestHandler<AddWalletCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddWalletCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddWalletCommand command, CancellationToken cancellationToken)
        {
            var wallet = new DomainModel.Wallet(command.Name, command.StartingValue, command.OwnerId, command.Ids);

            _unitOfWork.WalletsRepository.Add(wallet);
            wallet.UserWallets.SingleOrDefault(x => x.UserId == command.OwnerId)!.SetOwnership();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<int>(wallet.Id);
        }
    }
}
