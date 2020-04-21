using Core.Domain.Models;
using Core.Domain.Validations;

namespace FluentValidation
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> IsValid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, DomainSpecification<T> predicate) where T : Entity<T>
        {
            return ruleBuilder.Must(p => predicate.EhValido());
        }

        public static IRuleBuilderOptions<T, string> Date<T>(this IRuleBuilder<T, string> ruleBuilder, string expression)
        {
            return ruleBuilder.SetValidator(new DateValidator(expression));
        }

        public static IRuleBuilderOptions<T, string> Time<T>(this IRuleBuilder<T, string> ruleBuilder, string expression)
        {
            return ruleBuilder.SetValidator(new TimeValidator(expression));
        }

        public static IRuleBuilderOptions<T, string> Number<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NumberValidator());
        }

        public static IRuleBuilderOptions<T, string> Number<T>(this IRuleBuilder<T, string> ruleBuilder, long min, long max)
        {
            return ruleBuilder.SetValidator(new NumberValidator(min, max));
        }

        public static IRuleBuilderOptions<T, string> Decimal<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DecimalValidator());
        }

        public static IRuleBuilderOptions<T, string> Decimal<T>(this IRuleBuilder<T, string> ruleBuilder, decimal min, decimal max)
        {
            return ruleBuilder.SetValidator(new DecimalValidator(min, max));
        }
    }
}
