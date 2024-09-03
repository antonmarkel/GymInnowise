using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

namespace GymInnowise.UserService.API.Validators
{
    public class UpdateCoachProfileStatusRequestValidator : AbstractValidator<UpdateCoachProfileStatusRequest>
    {
        public UpdateCoachProfileStatusRequestValidator()
        {
            RuleFor(x => x.AccountId).Identifier();
            RuleFor(x => x.AccountStatus).AccountStatus();
            RuleFor(x => x.CoachStatus)
                .IsInEnum()
                .WithMessage("CoachStatus is invalid.");

            RuleFor(x => x.StatusNotes).Must(st => st != null)!.StatusNotes();
            RuleFor(x => x.ExpectedReturnDate)
                .Must(date => !date.HasValue || date.Value.Kind == DateTimeKind.Utc)
                .WithMessage("ExpectedReturnDate must be in UTC format.");
        }
    }
}
