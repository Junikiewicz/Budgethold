using FluentValidation;

namespace Budgethold.Application.Queries.Category.GetWalletCategory
{
    internal class GetWalletCategoryQueryValidator : AbstractValidator<GetWalletCategoryQuery>
    {
        public GetWalletCategoryQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}
