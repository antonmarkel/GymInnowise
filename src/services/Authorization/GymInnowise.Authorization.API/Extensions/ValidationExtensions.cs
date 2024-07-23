using FluentValidation;

namespace GymInnowise.Authorization.API.Extensions
{
    public static class ValidationExtensions
    {
        private const int PasswordMinLength = 8,
            PasswordMaxLength = 16,
            RefreshTokenLength = 88;

        public static IRuleBuilderOptions<T, string> RefreshToken<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(rf => rf.Length == RefreshTokenLength).WithMessage("Invalid refresh token!");
        }

        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .WithMessage("Your password cannot be empty!")
                .MinimumLength(PasswordMinLength)
                .WithMessage($"Your password length must be at least {PasswordMinLength}!")
                .MaximumLength(PasswordMaxLength)
                .WithMessage($"Your password length must not exceed {PasswordMinLength}!");
        }
    }
}
