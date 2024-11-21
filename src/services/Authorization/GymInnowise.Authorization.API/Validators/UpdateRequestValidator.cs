using FluentValidation;
using GymInnowise.Authorization.API.Extensions;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateRequestValidator()
        {
            RuleFor(rq => rq.Email).EmailAddress()
                .WithMessage("Invalid email address!");
            RuleFor(rq => rq.Password).Password();
            RuleFor(rq => rq.PhoneNumber).PhoneNumber();
        }
    }
}