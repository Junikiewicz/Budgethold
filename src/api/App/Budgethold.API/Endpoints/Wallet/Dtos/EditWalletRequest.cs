namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record EditWalletRequest
    {
        public EditWalletRequest()
        {
            Name = null!;
            UsersId = null!;
        }

        public IEnumerable<int> UsersId { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}
