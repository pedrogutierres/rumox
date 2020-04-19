using FluentValidation.Resources;
using FluentValidation.Validators;

namespace Core.Domain.Validations
{
    public class NumberValidator : PropertyValidator
    {
        private readonly bool _informouMinimoEMaximo = false;
        private readonly long _min;
        private readonly long _max;

        public NumberValidator() : base(new LanguageStringSource(nameof(NumberValidator)))
        {
        }
        public NumberValidator(long min, long max) : this()
        {
            _informouMinimoEMaximo = true;
            _min = min;
            _max = max;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return false;

            if (!long.TryParse((string) context.PropertyValue, out var number)) return false;

            if (_informouMinimoEMaximo)
                return number >= _min && number <= _max;

            return true;
        }
    }
}