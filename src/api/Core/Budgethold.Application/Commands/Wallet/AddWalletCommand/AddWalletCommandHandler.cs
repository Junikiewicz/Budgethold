using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;
using DomainModel = Budgethold.Domain.Models;

namespace Budgethold.Application.Commands.Wallet.AddWalletCommand
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
            var wallet = new DomainModel.Wallet(command.Name, command.StartingValue, command.UserId);

            _unitOfWork.WalletsRepository.Add(wallet);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<int>(wallet.Id);
        }
    }
}
