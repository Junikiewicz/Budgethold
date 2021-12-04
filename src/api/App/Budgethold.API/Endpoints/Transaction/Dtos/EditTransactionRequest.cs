namespace Budgethold.API.Endpoints.Transaction.Dtos
{
    public record EditTransactionRequest
    {
        public EditTransactionRequest()
        {
            Name = null!;
            Description = null!;
        }

        public string Name { get; init; }
        public string Description { get; init; }
        public int CategoryId { get; init; }
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
    }
}
