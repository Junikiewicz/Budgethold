using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Wallet.AddWalletCommand
{
    public class AddWalletCommand : IRequest<Result>
    {
        public AddWalletCommand(int userId, string name, decimal startingValue)
        {
            UserId = userId;
            Name = name;
            StartingValue = startingValue;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal StartingValue { get; set; }
    }
}
