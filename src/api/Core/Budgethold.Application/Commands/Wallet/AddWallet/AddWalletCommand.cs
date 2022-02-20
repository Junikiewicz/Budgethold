using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public record AddWalletCommand : IRequest<Result>
    {
        public AddWalletCommand(string name, decimal startingValue, int ownerId)
        {
            Name = name;
            StartingValue = startingValue;
            UserId = ownerId;
        }

        public int UserId { get; init; }

        public string Name { get; init; }

        public decimal StartingValue { get; init; }
    }
}
