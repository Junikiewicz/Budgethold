namespace Budgethold.Application.Queries.Category.GetWalletCategory
{
    public record CategoryResponse
    {
        public CategoryResponse(int id, string name, int? parentCategoryId, int transactionTypeId, int walletId)
        {
            Id = id;
            Name = name;
            ParentCategoryId = parentCategoryId;
            TransactionTypeId = transactionTypeId;
            WalletId = walletId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int? ParentCategoryId { get; init; }
        public int TransactionTypeId { get; init; }
        public int WalletId { get; init; }

    }
}
