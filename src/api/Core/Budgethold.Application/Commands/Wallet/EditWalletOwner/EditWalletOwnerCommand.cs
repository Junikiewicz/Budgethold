using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.EditWalletOwner
{
    public record EditWalletOwnerCommand : IRequest<Result>
    {
        public EditWalletOwnerCommand(int newOwnerId, int userId, int walletId)
        {
            NewOwnerId = newOwnerId;
            WalletId = walletId;
            UserId = userId;
        }

        public int NewOwnerId { get; init; }
        public int UserId { get; init; }
        public int WalletId { get; init; }
    }
}
