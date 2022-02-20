using FluentValidation;

namespace Budgethold.Application.Queries.Category.GetCategory
{
    internal class GetWalletCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetWalletCategoryQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}
