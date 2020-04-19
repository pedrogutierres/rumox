using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Globalization;

namespace Core.Domain.Validations
{
    public class TimeValidator : PropertyValidator
    {
        private readonly string _expression;

        public TimeValidator(string expression) : base(new LanguageStringSource(nameof(TimeValidator)))
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