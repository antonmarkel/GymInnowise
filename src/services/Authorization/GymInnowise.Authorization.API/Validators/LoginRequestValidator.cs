using FluentValidation;
using GymInnowise.Authorization.API.Extensions;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(rq => rq.Email).EmailAddress()
                .WithMessage("Invalid email address!");
            RuleFor(rq => rq.Password).Password();
        }
    }
}
