namespace Budgethold.API.Endpoints.Category.Dtos
{
    public record AddCategoryRequest
    {
        public AddCategoryRequest()
        {
            Name = null!;
        }

        public string Name { get; init; }
        public int? ParentCategoryId { get; init; }
        public int TransactionTypeId { get; init; }
    }
}
