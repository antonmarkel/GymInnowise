using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Updates
{
    public class UpdateClientProfileRequestValidator : AbstractValidator<UpdateClientProfileRequest>
    {
        public UpdateClientProfileRequestValidator()
        {
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.DateOfBirth).DateOfBirth();
            RuleFor(x => x.Gender).Must(gn => gn != null)!.Gender();
        }
    }
}