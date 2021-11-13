namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record AddWalletRequest
    {
        public AddWalletRequest()
        {
            Name = null!;
        }

        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}