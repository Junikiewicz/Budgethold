using FluentValidation;
using System;

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

        public static IRuleBuilderOptions<T, string> OneOrMoreDigits<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.");
        }

        public static IRuleBuilderOptions<T, string> OneOrMoreSpecialCharacters<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' must contain one or more special characters.");
        }

        public static IRuleBuilderOptions<T, int> CanBeCastedToEnum<T>(this IRuleBuilder<T, int> ruleBuilder, Type enumType)
        {
            return ruleBuilder.Must(x => Enum.IsDefined(enumType, x)).WithMessage("'{PropertyName}' value outside of valid range.");
        }
    }
}