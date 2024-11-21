using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Creates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Creates
{
    public class CreateClientProfileRequestValidator : AbstractValidator<CreateClientProfileRequest>
    {
        public CreateClientProfileRequestValidator()
        {
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.DateOfBirth).DateOfBirth();
            RuleFor(x => x.Gender).Must(gender => gender != null)!.Gender();
        }
    }
}