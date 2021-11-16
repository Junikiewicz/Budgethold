using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.AddWallet
{
    public record AddWalletCommand : IRequest<Result>
    {
        public AddWalletCommand(IEnumerable<int> ids, string name, decimal startingValue, int ownerId)
        {
            UserIds = ids;
            Name = name;
            StartingValue = startingValue;
            OwnerId = ownerId;
        }
        public int OwnerId { get; init; }
        public IEnumerable<int> UserIds { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}
