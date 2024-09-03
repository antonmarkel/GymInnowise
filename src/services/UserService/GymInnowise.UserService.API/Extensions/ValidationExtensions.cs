using FluentValidation;
using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.API.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .WithMessage("FirstName is required.")
                .Length(2, 50)
                .WithMessage("FirstName must be between 2 and 50 characters.");
        }

        public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .WithMessage("LastName is required.")
                .Length(2, 50)
                .WithMessage("LastName must be between 2 and 50 characters.");
        }

        public static IRuleBuilderOptions<T, string> Gender<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(gender => gender == null || new[] { "Male", "Female", "Other" }.Contains(gender))
                .WithMessage("Gender must be 'Male', 'Female', or 'Other' if provided.");
        }

        public static IRuleBuilderOptions<T, string> Goal<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Goal is required.")
                .Length(5, 200)
                .WithMessage("Goal must be between 5 and 200 characters.");
        }

        public static IRuleBuilderOptions<T, string> StatusNotes<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(500)
                .WithMessage("StatusNotes must not exceed 500 characters.");
        }

        public static IRuleBuilderOptions<T, DateTime> DateOfBirth<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .WithMessage("DateOfBirth is required.")
                .Must(date => date.Kind == DateTimeKind.Utc)
                .WithMessage("DateOfBirth must be in UTC format.")
                .LessThanOrEqualTo(DateTime.Today.ToUniversalTime())
                .WithMessage("DateOfBirth cannot be in the future.")
                .GreaterThan(DateTime.Today.AddYears(-120).ToUniversalTime())
                .WithMessage("DateOfBirth cannot indicate an age of more than 120 years.")
                .LessThan(DateTime.Today.AddYears(-16))
                .WithMessage("DateOfBirth cannot indicate an age of less than 16 years.");
        }

        public static IRuleBuilderOptions<T, Guid> Identifier<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .WithMessage("Identifier is required.");
        }

        public static IRuleBuilderOptions<T, ClientStatus> AccountStatus<T>(
            this IRuleBuilder<T, ClientStatus> ruleBuilder)
        {
            return ruleBuilder.IsInEnum()
                .WithMessage("AccountStatus is invalid.");
        }
    }
}
