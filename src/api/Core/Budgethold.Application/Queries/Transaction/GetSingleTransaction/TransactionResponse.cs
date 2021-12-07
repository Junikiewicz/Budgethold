namespace Budgethold.Application.Queries.Transaction.GetSingleTransaction
{
    public record TransactionResponse
    {
        public TransactionResponse()
        {
            Name = null!;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public int WalletId { get; init; }
        public int CategoryId { get; init; }
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
    }
}
