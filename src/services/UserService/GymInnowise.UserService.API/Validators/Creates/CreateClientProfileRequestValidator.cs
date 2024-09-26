using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;

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
