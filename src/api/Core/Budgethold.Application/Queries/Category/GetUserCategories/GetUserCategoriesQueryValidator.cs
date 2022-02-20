using FluentValidation;

namespace Budgethold.Application.Queries.Category.GetUserCategories
{
    public class GetUserCategoriesQueryValidator : AbstractValidator<GetUserCategoriesQuery>
    {
        public GetUserCategoriesQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
