namespace Budgethold.API.Endpoints.Category.Dtos
{
    public record EditCategoryRequest
    {
        public EditCategoryRequest()
        {
            Name = null!;
        }

        public string Name { get; init; }
        public int? ParentCategoryId { get; init; }
        public int? WalletId { get; init; }
        public int? TransactionTypeId { get; init; }
    }
}