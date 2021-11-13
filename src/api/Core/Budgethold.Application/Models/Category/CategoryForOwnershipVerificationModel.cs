namespace Budgethold.Application.Models.Category
{
    public record CategoryForOwnershipVerificationModel
    {
        public CategoryForOwnershipVerificationModel(int transactionTypeId, int walletId)
        {
            TransactionTypeId = transactionTypeId;
            WalletId = walletId;
        }

        public int TransactionTypeId { get; init; }
        public int WalletId { get; init; }
    }
}
