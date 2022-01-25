using FluentValidation;

namespace Budgethold.Application.Queries.Category.GetWalletCategories
{
    public class GetWalletCategoriesQueryValidator : AbstractValidator<GetWalletCategoriesQuery>
    {
        public GetWalletCategoriesQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
            RuleFor(x => x.WalletId).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0.");
        }
    }
}