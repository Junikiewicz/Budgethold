using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public record AddWalletCommand : IRequest<Result>
    {
        public AddWalletCommand(int userId, string name, decimal startingValue)
        {
            UserId = userId;
            Name = name;
            StartingValue = startingValue;
        }

        public int UserId { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}
