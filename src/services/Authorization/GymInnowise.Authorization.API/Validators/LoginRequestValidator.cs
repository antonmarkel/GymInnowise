using FluentValidation;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        private const int PasswordMinLength = 8, PasswordMaxLength = 16;

        public LoginRequestValidator()
        {
            RuleFor(rq => rq.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email address cannot be empty!")
                .EmailAddress().WithMessage("Invalid email!");
            RuleFor(rq => rq.Password).NotEmpty().WithMessage("Your password cannot be empty!")
                .MinimumLength(PasswordMinLength).WithMessage(
                    $"Your password length must be at least {PasswordMinLength}!")
                .MaximumLength(PasswordMaxLength).WithMessage(
                    $"Your password length must not exceed {PasswordMaxLength}!");
        }
    }
}
