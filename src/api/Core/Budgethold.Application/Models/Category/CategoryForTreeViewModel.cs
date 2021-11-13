namespace Budgethold.Application.Models.Category
{
    public record CategoryForTreeViewModel
    {
        public CategoryForTreeViewModel(int id, int? parentCategoryId, string name)
        {
            Id = id;
            ParentCategoryId = parentCategoryId;
            Name = name;
        }

        public int Id { get; init; }
        public int? ParentCategoryId { get; init; }
        public string Name { get; init; }
    }
}
