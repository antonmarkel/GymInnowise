using FluentValidation;
using GymInnowise.Authorization.API.Extensions;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(rq => rq.Email).EmailAddress()
                .WithMessage("Invalid email address!");
            RuleFor(rq => rq.Password).Password();
            RuleFor(rq => rq.PhoneNumber).PhoneNumber();
        }
    }
}