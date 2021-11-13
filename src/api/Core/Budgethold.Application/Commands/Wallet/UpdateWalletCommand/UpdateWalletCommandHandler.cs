using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;
using DomainModel = Budgethold.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Commands.Wallet.UpdateWalletCommand
{
    internal class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWalletCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateWalletCommand command, CancellationToken cancellationToken)
        {
            var wallet = new DomainModel.Wallet(command.Name, command.StartingValue,);

            _unitOfWork.WalletsRepository.Add(wallet);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Result<int>(wallet.Id);
        }
    }
}
