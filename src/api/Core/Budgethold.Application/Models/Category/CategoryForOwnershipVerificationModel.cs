namespace Budgethold.Application.Models.Category
{
    public record CategoryForOwnershipVerificationModel
    {
        public CategoryForOwnershipVerificationModel(int transactionTypeId, int userId)
        {
            TransactionTypeId = transactionTypeId;
            UserId = userId;
        }

        public int TransactionTypeId { get; init; }
        public int UserId { get; init; }
    }
}
