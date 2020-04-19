using FluentValidation.Resources;
using FluentValidation.Validators;

namespace Core.Domain.Validations
{
    public class DecimalValidator : PropertyValidator
    {
        private readonly bool _informouMinimoEMaximo = false;
        private readonly decimal _min;
        private readonly decimal _max;

        public DecimalValidator() : base(new LanguageStringSource(nameof(DecimalValidator)))
        {
        }
        public DecimalValidator(decimal min, decimal max) : this()
        {
            _informouMinimoEMaximo = true;
            _min = min;
            _max = max;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return false;

            if (!decimal.TryParse((string) context.PropertyValue, out var valor)) return false;

            if (_informouMinimoEMaximo)
                return valor >= _min && valor <= _max;

            return true;
        }
    }
}