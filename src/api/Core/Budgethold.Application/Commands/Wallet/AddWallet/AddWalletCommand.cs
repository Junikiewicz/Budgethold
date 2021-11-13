using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public record AddWalletCommand : IRequest<Result>
    {
        public AddWalletCommand(int[] users, string name, decimal startingValue, int ownerId)
        {
            Users = users;
            Name = name;
            StartingValue = startingValue;
            OwnerId = ownerId;
        }
        public int OwnerId { get; init; }
        public int[] Users { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}
