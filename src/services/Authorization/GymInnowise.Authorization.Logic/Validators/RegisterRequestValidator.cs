using FluentValidation;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.Logic.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public const int PasswordMinLength = 8, PasswordMaxLength = 16;

        public RegisterRequestValidator()
        {
            RuleFor(rq => rq.Email).EmailAddress();
            RuleFor(rq => rq.Password).NotEmpty().WithMessage("Your password cannot be empty.")
                .MinimumLength(PasswordMinLength)
                .WithMessage($"Your password length must be at least {PasswordMinLength}.")
                .MaximumLength(PasswordMaxLength)
                .WithMessage($"Your password length must not exceed {PasswordMinLength}.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(rq => rq.PhoneNumber).Matches(@"^\+(\d{1,3})\s?\(?\d{1,4}?\)?[\d\s\-]{7,}$")
                .WithMessage("Incorrect phone number.");
        }
    }
}