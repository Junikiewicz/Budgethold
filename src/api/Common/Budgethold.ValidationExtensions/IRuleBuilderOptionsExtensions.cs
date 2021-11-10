using FluentValidation;

namespace Budgethold.ValidationExtensions
{
    public static class IRuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, string> OneOrMoreCapitalLetters<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.");
        }

        public static IRuleBuilderOptions<T, string> OneOrMoreLowercaseLetters<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.");
        }

        public static IRuleBuilderOptions<T, string> OneOrMoreDigit<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.");
        }

        public static IRuleBuilderOptions<T, string> OneOrMoreSpecialCharaceters<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' must contain one or more special characters.");
        }
    }
}