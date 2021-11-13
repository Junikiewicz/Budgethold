namespace Budgethold.Application.Queries.Category.GetWalletCategories
{
    public record CategoryTreeResponse
    {
        public CategoryTreeResponse(int id, string name, IEnumerable<CategoryTreeResponse> childCategories)
        {
            Id = id;
            Name = name;
            Childrens = childCategories;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<CategoryTreeResponse> Childrens { get; init; }
    }
}
