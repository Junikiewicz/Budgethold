using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.AddWalletCommand
{
    public class AddWalletCommand : IRequest<Result>
    {
        public AddWalletCommand(int[] users, string name, decimal startingValue, int ownerId)
        {
            Users = users;
            Name = name;
            StartingValue = startingValue;
            OwnerId = ownerId;
        }

        public int[] Users { get; set; }
        public string Name { get; set; }
        public decimal StartingValue { get; set; }
        public int OwnerId { get; set; }
    }
}
