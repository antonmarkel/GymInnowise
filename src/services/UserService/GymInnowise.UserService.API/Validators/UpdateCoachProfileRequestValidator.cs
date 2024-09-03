using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

namespace GymInnowise.UserService.API.Validators
{
    public class UpdateCoachProfileRequestValidator : AbstractValidator<UpdateCoachProfileRequest>
    {
        public UpdateCoachProfileRequestValidator()
        {
            RuleFor(x => x.AccountId).Identifier();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).FirstName();
            RuleFor(x => x.DateOfBirth).DateOfBirth();
            RuleFor(x => x.Gender).Must(gr => gr != null)!.Gender();
        }
    }
}
