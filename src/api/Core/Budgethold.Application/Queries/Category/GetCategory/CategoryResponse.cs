namespace Budgethold.Application.Queries.Category.GetCategory
{
    public record CategoryResponse
    {
        public CategoryResponse(int id, string name, int? parentCategoryId, int transactionTypeId)
        {
            Id = id;
            Name = name;
            ParentCategoryId = parentCategoryId;
            TransactionTypeId = transactionTypeId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int? ParentCategoryId { get; init; }
        public int TransactionTypeId { get; init; }
    }
}
