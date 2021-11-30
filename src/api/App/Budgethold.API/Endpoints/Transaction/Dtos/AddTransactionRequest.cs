namespace Budgethold.API.Endpoints.Transaction.Dtos
{
    public class AddTransactionRequest
    {
        public AddTransactionRequest()
        {
            Name = null!;
            Description = null!;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int WalletId { get; private set; }
        public int CategoryId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
    }
}
