using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Globalization;

namespace Core.Domain.Validations
{
    public class DateValidator : PropertyValidator
    {
        private readonly string _expression;

        public DateValidator(string expression) : base(new LanguageStringSource(nameof(DateValidator)))
        {
            _expression = expression;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return false;

            return DateTime.TryParseExact((string)context.PropertyValue, _expression, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}