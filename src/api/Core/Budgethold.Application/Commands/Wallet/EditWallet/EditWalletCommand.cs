using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public record EditWalletCommand : IRequest<Result>
    {
        public EditWalletCommand(int walletId, string name, decimal startingValue, int userId, IEnumerable<int> usersId)
        {
            WalletId = walletId;
            UsersId = usersId;
            Name = name;
            StartingValue = startingValue;
            UserId = userId;
        }
        public int UserId { get; init; }
        public int WalletId { get; init; }
        public IEnumerable<int> UsersId { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}

