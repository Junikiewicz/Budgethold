using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.EditWallet
{
    public class EditWalletCommand : IRequest<Result>
    {
        public EditWalletCommand(int walletId, string name, decimal startingValue, int userId, int[] users)
        {
            WalletId = walletId;
            Users = users;
            Name = name;
            StartingValue = startingValue;
            UserId = userId;
        }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int[] Users { get; set; }
        public string Name { get; set; }
        public decimal StartingValue { get; set; }
    }
}

