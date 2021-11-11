namespace Budgethold.Application.Queries.Wallet.GetUserWalletsQuery
{
    public record WalletResponse
    {
        public int Id { get; init; }
        public decimal StartingValue { get; init; }
        public decimal CurrentValue { get; init; }
        public string? Name { get; init; }
        public IEnumerable<OwningUser>? OwningUsers { get; init; }

        public record OwningUser
        {
            public int Id { get; init; }
            public string? Name { get; init; }
        }
    }
}
