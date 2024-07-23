using FluentValidation;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.Logic.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(rq => rq.Email).EmailAddress();
            RuleFor(rq => rq.Password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(RegisterRequestValidator.PasswordMinLength).WithMessage(
                    $"Your password length must be at least {RegisterRequestValidator.PasswordMinLength}.")
                .MaximumLength(RegisterRequestValidator.PasswordMaxLength).WithMessage(
                    $"Your password length must not exceed {RegisterRequestValidator.PasswordMinLength}.");
        }
    }
}
