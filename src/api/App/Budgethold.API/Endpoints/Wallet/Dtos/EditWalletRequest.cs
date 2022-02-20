namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record EditWalletRequest
    {
        public EditWalletRequest()
        {
            Name = null!;
        }

        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}
