using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

namespace GymInnowise.UserService.API.Validators
{
    public class UpdateClientProfileRequestValidator : AbstractValidator<UpdateClientProfileRequest>
    {
        public UpdateClientProfileRequestValidator()
        {
            RuleFor(x => x.AccountId).Identifier();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.DateOfBirth).DateOfBirth();
            RuleFor(x => x.Gender).Must(gn => gn != null)!.Gender();
        }
    }
}
