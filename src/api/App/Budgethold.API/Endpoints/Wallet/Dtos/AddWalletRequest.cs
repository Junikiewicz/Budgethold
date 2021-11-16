namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record AddWalletRequest
    {
        public AddWalletRequest()
        {
            Name = null!;
            UserIds = null!;
        }
        public IEnumerable<int> UserIds { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}