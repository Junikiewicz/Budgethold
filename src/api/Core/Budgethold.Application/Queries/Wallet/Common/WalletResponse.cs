namespace Budgethold.Application.Queries.Wallet.Common
{
    public record WalletResponse
    {
        public WalletResponse()
        {
            Name = null!;
        }

        public int Id { get; init; }
        public decimal StartingValue { get; init; }
        public decimal CurrentValue { get; init; }
        public string Name { get; init; }
    }
}
